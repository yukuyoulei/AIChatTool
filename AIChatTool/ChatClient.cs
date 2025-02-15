using AIChatTool.FunctionCalls;
using Cysharp.Threading.Tasks;

namespace AIChatTool;

public class ChatClient
{
    public ChatRequest ChatRequest;

    public ChatClient()
    {
        ChatRequest = new ChatRequest();
        ChatRequest.Messages = new List<Message>
        {
            new()
            {
                Role = "system",
                Content = "You are a helpful assistant."
            }
        };
        ChatRequest.Model = "deepseek-chat";
        ChatRequest.FrequencyPenalty = 0;
        ChatRequest.MaxTokens = 2048;
        ChatRequest.PresencePenalty = 0;
        ChatRequest.ResponseFormat = new ResponseFormat
        {
            Type = "text"
        };
        ChatRequest.Stop = null;
        ChatRequest.Stream = false;
        ChatRequest.StreamOptions = null;
        ChatRequest.Temperature = 1;
        ChatRequest.TopP = 1;
        ChatRequest.Tools = null;
        ChatRequest.ToolChoice = "auto";
        ChatRequest.Logprobs = false;
        ChatRequest.TopLogprobs = null;


        var tools = new List<Tool>();
        ChatRequest.Tools = tools;
        tools.Add(new Tool
        {
            Type = "function",
            Function = FunctionCallGetWeather.Function
        });
    }

    public async UniTask<string> SendChatMsg(string msg)
    {
        ChatRequest.Messages.Add(new Message
        {
            Role = "user",
            Content = msg
        });

        var chatCompletion = await ChatUtil.SendChatMsg(ChatRequest);
        var result = chatCompletion.Choices[0].Message;
        ChatRequest.Messages.Add(result);
        return result.Content;
    }
}