using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LCSExercise.Model
{

    public class Value
    {
        public string value { get; set; }
    }
    public class LCSRequest
    {
        public List<Value> values { get; set; }
    }

    public class LCSResponse
    {
        public List<Value> lcs { get; set; }
    }
}
