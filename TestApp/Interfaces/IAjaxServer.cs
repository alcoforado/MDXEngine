using System;
namespace TestApp
{
    interface IAjaxServer 
    {
        string JavascriptRequest(string urlPath, string data);
        bool JavascriptRequestAsync(string urlPath, string request_id, string data);
    }
}
