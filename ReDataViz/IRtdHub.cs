using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace ReDataViz
{
    public interface IRtdHub
    {
        Task Add(Models.DtvizMessage dtm);
        //{ return await Clients.All.PostDtm(dtm); }
        Task Delete(Models.DtvizMessage dtm); //{ return await Clients.All.DeleteDtm(dtm); }
    }

    public class RtdHub : Hub<IRtdHub>
    {
        
        public async Task Add(Models.DtvizMessage dtm)
        {
            await Clients.All.Add(dtm);
        }

        
        public async Task Delete(Models.DtvizMessage dtm)
        {
            await Clients.All.Delete(dtm);
        }
    }
}
