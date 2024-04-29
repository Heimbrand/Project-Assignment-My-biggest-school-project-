using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using HagaDropsIt.Client.ChatBot.Interfaces;
using System.Text.RegularExpressions;
using System.Text.RegularExpressions;
using HagaDropsIt.Client.ChatBot.Entities;

namespace HagaDropsIt.Client.ChatBot.Services
{
    public class ChatService : IChatService
    {
        private readonly Kernel _kernel;
        private readonly ILogger<ChatService> _logger;
        public event Func<ChatMessage, Task> MessageReceived;
        private ChatHistory history;
        private readonly ChatMessage _chatMessage = new ChatMessage();

        public ChatService(Kernel kernel, ILogger<ChatService> logger)
        {   
            history = new ChatHistory();
            MessageReceived += (ChatMessage) =>
            {
                _logger.LogInformation(ChatMessage.Text, ChatMessage.CssClass, ChatMessage.ImageUrl);
                return Task.CompletedTask;
            };
            _kernel = kernel;
            _logger = logger;


        }

        public async Task<ChatMessage> GetResponseAsync(string userInput)
        {

            try
            {
                var chatCompletionService = _kernel.GetRequiredService<IChatCompletionService>();
                history.AddUserMessage(userInput);

                ChatMessageContent chatResult = await chatCompletionService.GetChatMessageContentAsync(history,
                    new OpenAIPromptExecutionSettings { ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions }, _kernel);


                ChatMessage response;
                if (chatResult.Content != null)
                {
                    history.AddMessage(chatResult.Role, chatResult.Content ?? string.Empty);

                    _logger.LogInformation($"\n>>> Result: {chatResult.Content}\n");
                    response = new ChatMessage { Text = chatResult.Content, CssClass =  "ai-response", ImageUrl = ExtractImageUrl(chatResult.Content) };
                }
                else
                {
                    _logger.LogInformation("\n>>> Result: No content\n");
                    response = new ChatMessage { Text = "I'm sorry, I'm having trouble processing your request. Please try again later.", CssClass = "ai-response" };
                }

                await MessageReceived?.Invoke(response);

                return response;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing user input");
                return new ChatMessage { Text = "I'm sorry, I'm having trouble processing your request. Please try again later.", CssClass = "ai-response" };
            }
        }

        public string? ExtractImageUrl(string message)
        {
           
            var match = Regex.Match(message, @"https:\/\/\S+\.(png|jpg|jpeg|gif)");
            return match.Success ? match.Value : null;
        }
    }


    





}
