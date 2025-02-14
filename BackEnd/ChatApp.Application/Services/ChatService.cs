using ChatApp.Application.CQRS.Requests.Chat.Models;
using ChatApp.Application.Utilities;
using ChatApp.Core.Entities;
using ChatApp.Application.CQRS.ChatMessage.Commands.Models;
using ChatApp.Application.CQRS.ChatMessage.Commands.Response;
using ChatApp.Application.CQRS.ChatMessage.Queries.Response;
using ChatApp.Application.Services.inteface;
using ChatApp.Application.Utilities.Class;
using ChatApp.Core.Interfaces.Main;
using ChatApp.Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using ChatApp.Application.CQRS.ChatRoom.Commands.Models;
using ChatApp.Application.CQRS.ChatRoom.Queries.Models;
using ChatApp.Application.CQRS.ChatRoom.Queries.Response;

namespace ChatApp.Application.Services
{
    public class ChatService : IChatService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ChatService(IChatMessageRepository chatMessageRepository, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }



        public async Task<ApiResponse<ChatMessageResponse>> SendMessageAsync(SendMessageRequest request)
        {
            var message = new ChatMessage
            {
                Id = Guid.NewGuid(),
                UserId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier),
                ChatRoomId = request.ChatRoomId,
                Message = request.Message,
                Timestamp = DateTime.UtcNow,
                IsBotMessage = false,
            };

            await _unitOfWork.ChatMessageRepository.AddAsync(message);
            await _unitOfWork.CompleteAsync();

            return ResponseHandler.Success(new ChatMessageResponse
            {
                MessageId = message.Id,
                Message = message.Message,
                Sender = request.UserName,
                Timestamp = message.Timestamp
            }, "Message sent successfully.");
        }

        public async Task<ApiResponse<EditMessageResponse>> EditMessageAsync(EditMessageRequest request)
        {
            var message = await _unitOfWork.ChatMessageRepository.GetAsync(c=>c.Id == request.MessageId);
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

            if (message == null)
                return ResponseHandler.Failure<EditMessageResponse>("Message not found.");

            if (message.UserId != userId)
                return ResponseHandler.Failure<EditMessageResponse>("You can only edit your own messages.");

            message.Message = request.NewContent;
            _unitOfWork.ChatMessageRepository.Update(message);
            await _unitOfWork.CompleteAsync();

            return ResponseHandler.Success(new EditMessageResponse
            {
                MessageId = message.Id,
                NewContent = message.Message,
                EditedAt = message.Timestamp
            }, "Message edited successfully.");
        }

        public async Task<ApiResponse<DeleteMessageResponse>> DeleteMessageAsync(DeleteMessageRequest request)
        {
            var message = await _unitOfWork.ChatMessageRepository.GetAsync(c=>c.Id ==request.MessageId);
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            if (message == null)
                return ResponseHandler.Failure<DeleteMessageResponse>("Message not found.");

            if (message.UserId != userId)
                return ResponseHandler.Failure<DeleteMessageResponse>("You can only delete your own messages.");

            _unitOfWork.ChatMessageRepository.Remove(message);
            await _unitOfWork.CompleteAsync();

            return ResponseHandler.Success(new DeleteMessageResponse
            {
                MessageId = request.MessageId,
                DeletedAt = DateTime.UtcNow
            }, "Message deleted successfully.");
        }

        public async Task<ApiResponse<IList<ChatMessageResponse>>> GetChatRoomMessagesAsync(Guid chatRoomId)
        {
            var messages = await _unitOfWork.ChatMessageRepository.GetMessagesByChatRoomAsync(chatRoomId);

            if (messages == null || !messages.Any())
            {
                return ResponseHandler.Failure<IList<ChatMessageResponse>>("No messages found for this chat room.");
            }

            var response = messages.Select(m => new ChatMessageResponse
            {
                MessageId = m.Id,
                Message = m.Message,
                Sender = m.User.UserName,
                Timestamp = m.Timestamp
            }).ToList();

            return ResponseHandler.Success<IList<ChatMessageResponse>>(response, "Messages retrieved successfully.");
        }

        public async Task<string> JoinRoomAsync(JoinRoomRequest request)
        {

            var member = new ChatRoomMember
            {
                ChatRoomId = request.ChatRoomId,
                UserId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier)
            };

            await _unitOfWork.ChatRoomMemberRepository.AddAsync(member);
            await _unitOfWork.CompleteAsync();

            return "Joined room successfully.";

        }
        public async Task<ApiResponse<ChatRoomResponse>> LeaveRoomAsync(LeaveRoomRequest request)
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return ResponseHandler.Failure<ChatRoomResponse>("User not found.");

            var roomUser = await _unitOfWork.ChatRoomMemberRepository.GetAsync(c => c.UserId == userId && c.ChatRoomId == request.ChatRoomId);
            if (roomUser == null)
                return ResponseHandler.Failure<ChatRoomResponse>("User is not a member of this chat room.");

            _unitOfWork.ChatRoomMemberRepository.Remove(roomUser);
            await _unitOfWork.CompleteAsync();

            var response = new ChatRoomResponse
            {
                ChatRoomId = request.ChatRoomId,
                RoomName = "Test Room",
                Participants = new List<string>(),
                LastActive = DateTime.UtcNow
            };

            return ResponseHandler.Success(response, $"{request.UserName} has left {request.ChatRoomId}.");
        }



        public async Task<ApiResponse<List<ChatRoomAllResponse>>> GetAllRoomAsync(GetAllRoomsRequest request)
        {
            var rooms = await _unitOfWork.ChatRoomRepository.GetAllAsync();

            var roomResponses = rooms.Select(room => new ChatRoomAllResponse
            {
                Id = room.Id.ToString(),
                Name = room.Name
            }).ToList(); 

            return ResponseHandler.Success(roomResponses, "Rooms retrieved successfully.");
        }


    }
}
