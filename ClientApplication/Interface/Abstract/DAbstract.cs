using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClientApplication.Interface
{
    public abstract class DAbstract
    {

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

    }
}
