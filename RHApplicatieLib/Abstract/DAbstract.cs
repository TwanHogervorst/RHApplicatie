using Newtonsoft.Json;

namespace RHApplicationLib.Abstract
{
    public abstract class DAbstract
    {

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

    }
}
