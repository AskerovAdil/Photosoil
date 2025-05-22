using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Photosoil.Core.Models;
using Photosoil.Service.Data;
using Photosoil.Service.Helpers;
using Photosoil.Service.Helpers.ViewModel.Request;
using Photosoil.Service.Helpers.ViewModel.Response;
using System.Threading.Tasks;

namespace Photosoil.Service.Services
{
    public class AuthorService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly EmailService _emailService;
        private readonly IConfiguration _configuration;
        private readonly string _adminEmail;

        public AuthorService(ApplicationDbContext context, IMapper mapper, EmailService emailService, IConfiguration configuration)
        {
            _mapper = mapper;
            _context = context;
            _emailService = emailService;
            _configuration = configuration;
            _adminEmail = "askerov121099@gmail.com"; // Фиксированный email для получения заявок
        }

        public ServiceResponse<List<AuthorResponse>> GetAdminAll(int userId = 0, string role = "")
        {
            IQueryable<Author> listAuthor;
            if (role == "Moderator")
                listAuthor = _context.Author.Include(x => x.Photo).Include(x => x.User).Include(x => x.DataRu).Include(x => x.DataEng).Where(x => x.UserId == userId);
            else if (role == "Admin")
                listAuthor = _context.Author.Include(x => x.Photo).Include(x=>x.User).Include(x => x.DataRu).Include(x => x.DataEng);
            else
                listAuthor = Enumerable.Empty<Author>().AsQueryable();

            var result = new List<AuthorResponse>();
            foreach (var el in listAuthor)
                result.Add(_mapper.Map<AuthorResponse>(el));

            return ServiceResponse<List<AuthorResponse>>.OkResponse(result);
        }

        public  ServiceResponse<List<AuthorResponse>> Get()
        {
            var listAuthor = _context.Author.Include(x => x.Photo).Include(x => x.User).Include(x => x.DataRu).Include(x => x.DataEng).ToList();
            
            var result = new List<AuthorResponse>();
            foreach (var el in listAuthor)
                result.Add(_mapper.Map<AuthorResponse>(el));

            return ServiceResponse<List<AuthorResponse>>.OkResponse(result);
        }

        public ServiceResponse<AuthorResponseById> GetById(int id)
        { 
                var author = _context.Author
                .AsNoTracking()     
                .Include(x => x.DataRu).Include(x => x.DataEng)
                .Include(x => x.EcoSystems).ThenInclude(x => x.Photo)
                .Include(x => x.EcoSystems).ThenInclude(x => x.Translations)
                .Include(x => x.SoilObjects).ThenInclude(x => x.Photo)
                .Include(x => x.SoilObjects).ThenInclude(x => x.Translations)
                .Include(x => x.SoilObjects).ThenInclude(x => x.User)
                .Include(x => x.User)
                .Include(x=>x.Photo).AsNoTracking().FirstOrDefault(x=>x.Id == id);


            var result = _mapper.Map<AuthorResponseById>(author);

           //result.Contact = JsonConvert.DeserializeObject<string[]?>(author.Contact);
           //result.OtherPrifile = JsonConvert.DeserializeObject<string[]?>(author.OtherPrifile);

            return author != null 
                ? ServiceResponse<AuthorResponseById>.OkResponse(result) 
                : ServiceResponse<AuthorResponseById>.BadResponse(ErrorMessage.NoContent);
        }

        public async Task<ServiceResponse<Author>> Post(int userId, AuthorVM authorVM)
        {
            try
            {
                var author = _mapper.Map<Author>(authorVM);
                author.PhotoId = authorVM.PhotoId;
                author.UserId = userId;
                author.CreatedDate = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                _context.Author.Add(author);
                await _context.SaveChangesAsync();
                
                return ServiceResponse<Author>.OkResponse(author);
            }
            catch (Exception ex)
            {
                return ServiceResponse<Author>.BadResponse(ex.Message);
            }
        }

        public async Task<ServiceResponse<Author>> Put(int id, AuthorVM authorVM)
        {
            try
            {
                var author = _context.Author.FirstOrDefault(x => x.Id == id);
                
                _mapper.Map(authorVM, author);
                author.PhotoId = authorVM.PhotoId;

                _context.Author.Update(author);
                _context.SaveChanges();
                return ServiceResponse<Author>.OkResponse(author);
            }
            catch (Exception ex)
            {
                return ServiceResponse<Author>.BadResponse(ex.Message);
            }
        }

        public ServiceResponse Delete(int id)
        {
            var author = _context.Author.Include(x => x.DataRu).Include(x => x.DataEng).Include(x=>x.Photo).FirstOrDefault(x => x.Id == id);

            try
            {
                if (author != null) _context.Author.Remove(author);
                _context.SaveChanges();
                return ServiceResponse.OkResponse;
            }
            catch (Exception ex)
            {
                return ServiceResponse.BadResponse(ex.Message);
            }
        }

        /// <summary>
        /// Обработка заявки на роль автора
        /// </summary>
        public async Task<ServiceResponse> BecomeAuthorAsync(AuthorRequest model)
        {
            // Формирование HTML-сообщения с информацией о заявке
            var message = $@"
                <html>
                <head>
                    <style>
                        body {{ font-family: Arial, sans-serif; line-height: 1.6; }}
                        .container {{ max-width: 600px; margin: 0 auto; padding: 20px; }}
                        .header {{ background-color: #4CAF50; color: white; padding: 10px; text-align: center; }}
                        .content {{ padding: 20px; border: 1px solid #ddd; }}
                        .field {{ margin-bottom: 10px; }}
                        .field-name {{ font-weight: bold; }}
                    </style>
                </head>
                <body>
                    <div class='container'>
                        <div class='header'>
                            <h2>Новая заявка на роль автора</h2>
                        </div>
                        <div class='content'>
                            <div class='field'>
                                <div class='field-name'>ФИО:</div>
                                <div>{model.Name}</div>
                            </div>
                            <div class='field'>
                                <div class='field-name'>Организация:</div>
                                <div>{(string.IsNullOrEmpty(model.Organization) ? "Не указана" : model.Organization)}</div>
                            </div>
                            <div class='field'>
                                <div class='field-name'>Должность:</div>
                                <div>{(string.IsNullOrEmpty(model.Position) ? "Не указана" : model.Position)}</div>
                            </div>
                            <div class='field'>
                                <div class='field-name'>Email:</div>
                                <div>{model.Email}</div>
                            </div>
                        </div>
                    </div>
                </body>
                </html>";

            try
            {
                // Отправка email администратору
                await _emailService.SendEmailAsync(
                    _adminEmail,
                    $"Новая заявка на роль автора от {model.Name}",
                    message);

                // Отправка подтверждения пользователю
                var confirmationMessage = $@"
                    <html>
                    <head>
                        <style>
                            body {{ font-family: Arial, sans-serif; line-height: 1.6; }}
                            .container {{ max-width: 600px; margin: 0 auto; padding: 20px; }}
                            .header {{ background-color: #4CAF50; color: white; padding: 10px; text-align: center; }}
                            .content {{ padding: 20px; border: 1px solid #ddd; }}
                        </style>
                    </head>
                    <body>
                        <div class='container'>
                            <div class='header'>
                                <h2>Заявка на роль автора принята</h2>
                            </div>
                            <div class='content'>
                                <p>Здравствуйте, {model.Name}!</p>
                                <p>Ваша заявка на роль автора в системе Photosoil успешно принята.</p>
                                <p>Мы рассмотрим вашу заявку и свяжемся с вами в ближайшее время.</p>
                                <p>С уважением,<br>Команда Photosoil</p>
                            </div>
                        </div>
                    </body>
                    </html>";

                await _emailService.SendEmailAsync(
                    model.Email,
                    "Заявка на роль автора в системе Photosoil",
                    confirmationMessage);

                return ServiceResponse.OkResponse;
            }
            catch (Exception ex)
            {
                return ServiceResponse.BadResponse($"Ошибка при отправке заявки: {ex.Message}");
            }
        }
    }
}
