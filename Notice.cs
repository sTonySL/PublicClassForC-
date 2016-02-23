using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jwc
{
    class Notice
    {
        private String _title;
        private DateTime _date;
        private String _content;
        public String Title
        {
            get { return _title; }
            set { _title = value; }
        }
        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }
        public String Content
        {
            get { return _content; }
            set { _content = value; }
        }
    }
}
