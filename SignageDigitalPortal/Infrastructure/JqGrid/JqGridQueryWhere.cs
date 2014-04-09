using System.Collections.Generic;

namespace Infrastructure.JqGrid
{
    public class JqGridQueryWhere
    {
        public string Query { get; set; }
        public List<string> LstParams { get; set; }
        public string[] ArrParams {
            get
            {
                return LstParams.ToArray();
            }
        }

        public JqGridQueryWhere()
        {
            LstParams = new List<string>();
        }
    }
}