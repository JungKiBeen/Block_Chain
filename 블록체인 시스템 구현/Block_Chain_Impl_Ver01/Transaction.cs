using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Block_Chain_Impl_Ver01
{
    [Serializable]
    public class Transaction
    {
        public string data { get; set; }
        public string from_user { get; set; }
        public string time_stamp { get; set; }

        public Transaction(string data, string from_user, string time_stamp)
        {
            this.data = data;
            this.from_user = from_user;
            this.time_stamp = time_stamp;
        }

        public string get_transaction()
        {
            return data + from_user + time_stamp;
        }
    }
}
