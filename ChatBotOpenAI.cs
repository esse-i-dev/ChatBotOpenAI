// ChatBotOpenAI
using OpenAI.Managers;
using OpenAI;
using Microsoft.Extensions.DependencyInjection;
using OpenAI.ObjectModels.RequestModels;
using OpenAI.ObjectModels;


// Carico l'API KEY di OPENAI
var openAiService = new OpenAIService(new OpenAiOptions()
{
    ApiKey = "<INSERT_YOUR_OPENAI_API_KEY_HERE>"
});


var HistoricalConversation = new List<ChatMessage>
    {
        ChatMessage.FromSystem(
            "Tu sei .NetBot, un versatile assistente progettato per aiutare i programmatori in C#, nel framework .NET, in SQL Server, su Azure e, più in generale, su tecnologie Microsoft. Sei simpatico, affabile e fornisci risposte corrette."
            ),
    };

// Ciclo infinito
while (true)

{

    // Scrivo in console
    Console.WriteLine("Di cosa vuoi parlare adesso? (premi CTRL + C per uscire)");


    // Leggo l'input dell'utente
    string question = Console.ReadLine();


    // Aggiungo la domanda alla lista che contiene la conversazione
    HistoricalConversation.Add(ChatMessage.FromUser(question));


    var completionResult = await openAiService.ChatCompletion.CreateCompletion(new ChatCompletionCreateRequest
    {
        Messages = HistoricalConversation,
        Model = Models.Gpt_4,
        MaxTokens = 4096, //optional
        Temperature = (float?)0.7
    });
    if (completionResult.Successful)
    {
        Console.WriteLine(completionResult.Choices.First().Message.Content);
    }

    // Aggiungo la risposta di OpenAI alla lista che contiene la conversazione
    HistoricalConversation.Add(ChatMessage.FromAssistant(completionResult.Choices.First().Message.Content));

};
