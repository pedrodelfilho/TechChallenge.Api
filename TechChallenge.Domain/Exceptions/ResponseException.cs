using TechChallenge.Domain.Entities.Responses;

namespace TechChallenge.Domain.Exceptions
{
    public static class ResponseException
    {
        public static BaseResponse QueueErrorMessage()
        {
            return new BaseResponse
            {
                Message = "Ocorreu algum erro no serviço de mensageria.",
                Success = false,
                Errors = null
            };
        }

        public static BaseResponse ApplicationErrorMessage()
        {
            return new BaseResponse
            {
                Message = "Ocorreu algum erro interno na aplicação, por favor tente novamente.",
                Success = false,
                Errors = null
            };
        }

        public static BaseResponse DomainErrorMessage(string message)
        {
            return new BaseResponse
            {
                Message = message,
                Success = false,
                Errors = null
            };
        }

        public static BaseResponse DomainErrorMessage(string message, List<string> errors)
        {
            return new BaseResponse
            {
                Message = message,
                Success = false,
                Errors = errors
            };
        }

        public static BaseResponse UnauthorizedErrorMessage()
        {
            return new BaseResponse
            {
                Message = "A combinação de login e senha está incorreto!",
                Success = false,
                Errors = null
            };
        }
    }
}
