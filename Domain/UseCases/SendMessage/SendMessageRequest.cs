using Domain.Entity;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UseCases.SendMessage
{
    public class SendMessageRequest : IRequest<SendMessageRequestResponse>
    {
        public MessageEntity Message { get; set; }
    }

    public class SendMessageRequestResponse
    {
        public bool Sucess { get; set; }
    }

    public class SendMessageRequestHandler : IRequestHandler<SendMessageRequest, SendMessageRequestResponse>
    {
        private readonly IMessageService messageService;

        public SendMessageRequestHandler(IMessageService messageService)
        {
            this.messageService = messageService;
        }

        public async Task<SendMessageRequestResponse> Handle(SendMessageRequest request, CancellationToken cancellationToken)
        {
            ValidateRequest(request);
            request.Message.Date = DateTime.Now;

            await messageService.PostMessage(request.Message);

            return new SendMessageRequestResponse { Sucess = true };
        }

        public void ValidateRequest(SendMessageRequest request)
        {
            if (string.IsNullOrEmpty(request.Message.MyNick))
                throw new ArgumentException("MyNick precisa ser preenchido");

            if (string.IsNullOrEmpty(request.Message.FriendNick))
                throw new ArgumentException("FriendNick precisa ser preenchido");

            if (string.IsNullOrEmpty(request.Message.Message))
                throw new ArgumentException("Message precisa ser preenchido");
        }
    }
}