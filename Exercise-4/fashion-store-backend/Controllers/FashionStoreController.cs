using Microsoft.AspNetCore.Mvc;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace fashion_store_backend.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class FashionStoreController : ControllerBase
    {
        private readonly IChatCompletionService _chat;
        private readonly ChatHistory _history;
        private readonly Kernel _kernel;

        public FashionStoreController()
        {
            var modelPath = @"/app/cpu_and_mobile/cpu-int4-rtn-block-32";

            // Set up kernel with ONNX runtime and initialize chat service
            var builder = Kernel.CreateBuilder();
            builder.AddOnnxRuntimeGenAIChatCompletion(modelPath: modelPath);
            _kernel = builder.Build();

            _chat = _kernel.GetRequiredService<IChatCompletionService>();
            _history = new ChatHistory();
            _history.AddSystemMessage("You are an AI assistant that helps people find concise information about products. Keep your responses brief and focus on key points.");

        }

        [HttpPost("GenerateResponse")]
        public async Task GenerateResponse([FromBody] ChatRequest request)
        {
            string promptTemplate = $"{request.UserPrompt} Product: {request.ProductName}. Description: {request.ProductDescription}";
            Console.WriteLine(promptTemplate);
            _history.AddUserMessage(promptTemplate);

            var promptSettings = new OpenAIPromptExecutionSettings
            {
                Temperature = 0.7,
                MaxTokens = 300, // Adjust token limit as needed
                TopP = 0.95
            };

            var result = _chat.GetStreamingChatMessageContentsAsync(
                _history,
                executionSettings: promptSettings,
                kernel: _kernel
            );

            Response.ContentType = "text/plain";
            string response = "";
            await foreach (var message in result)
            {
                response += message.Content;
                Console.Write(message.Content);
                await Response.WriteAsync(message.Content);
                await Response.Body.FlushAsync();
            }

            _history.AddAssistantMessage(response);
        }

    }

    public class ChatRequest
    {
        public string UserPrompt { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
    }
}
