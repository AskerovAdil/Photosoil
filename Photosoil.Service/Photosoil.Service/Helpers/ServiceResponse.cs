using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photosoil.Service.Helpers
{
    public class ServiceResponse
    {
        /// <summary>
        /// Есть ли ошибка
        /// </summary>
        public bool Error { get; set; } = false;

        /// <summary>
        /// Сообщение об ошибке
        /// </summary>
        public string Message { get; set; } = "";

        /// <summary>
        /// Конструктор успешного запроса
        /// </summary>
        public static ServiceResponse OkResponse = new ServiceResponse();

        /// <summary>
        /// Конструктор плохого запроса
        /// </summary>
        /// <param name="message">Сообщение об ошибке</param>
        /// <returns></returns>
        public static ServiceResponse BadResponse(string message)
        {
            return new ServiceResponse() { Error = true, Message = message };
        }

        /// <summary>
        /// Проверяет есть ли ошибки в запросе, если есть выполняет функцию
        /// </summary>
        /// <param name="errorFunc">Функция вызываемая при ошибке</param>
        public void CheckError(Action errorFunc)
        {
            if (Error)
            {
                errorFunc();
            }
        }

        /// <summary>
        /// Проверяет есть ли ошибки в запросе, если есть выполняет функцию
        /// </summary>
        /// <param name="errorFunc">Функция вызываемая при ошибке</param>
        public void CheckError(Action<ServiceResponse> errorFunc)
        {
            if (Error)
            {
                errorFunc(this);
            }
        }
    }

    public class ServiceResponse<T> : ServiceResponse
    {
        /// <summary>
        /// Обьект для ответа
        /// </summary>
        public T Response { get; set; }

        /// <summary>
        /// Конструктор запроса с ошибкой
        /// </summary>
        /// <param name="message">Сообщение об ошибке</param>
        /// <returns></returns>
        public static ServiceResponse<T> BadResponse(string message)
        {
            return new ServiceResponse<T> { Error = true, Message = message };
        }

        /// <summary>
        /// Конструктор запроса без ошибки
        /// </summary>
        /// <param name="responseObj">Возвращаемое значение</param>
        /// <returns></returns>
        public static ServiceResponse<T> OkResponse(T responseObj)
        {
            return new ServiceResponse<T> { Error = false, Message = "", Response = responseObj };
        }

        /// <summary>
        /// Проверяет успешен ли запрос. Если да - возвращает данные, Если нет, выполяет функцию
        /// </summary>
        /// <param name="errorFunc">Функция выполняемая при неудачном запросе</param>
        /// <returns></returns>
        public T ReturnOrCheckError(Action<ServiceResponse<T>> errorFunc)
        {
            if (Error || Response == null)
            {
                errorFunc(this);
            }

            return Response;
        }

        /// <summary>
        /// Проверяет успешен ли запрос. Если да - возвращает данные, Если нет, выполяет функцию
        /// </summary>
        /// <param name="errorFunc">Функция выполняемая при неудачном запросе</param>
        /// <returns></returns>
        public T ReturnOrCheckError(Action errorFunc)
        {
            if (Error || Response == null)
            {
                errorFunc();
            }

            return Response;
        }

        public ServiceResponse ToServiceResponse()
        {
            return new ServiceResponse() { Error = this.Error, Message = this.Message };
        }
    }
}
