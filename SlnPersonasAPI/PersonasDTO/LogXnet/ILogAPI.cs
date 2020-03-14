using System;
using System.Collections.Generic;
using System.Text;

namespace PersonasDTO.LogXNet
{
    public  interface ILogAPI
    {

       void Information(string message, Type typeClass);
       void Debug(string message, Type typeClass);
       void Warning(string message, Type typeClass);
        void Error(string message, Type typeClass);

    }
}
