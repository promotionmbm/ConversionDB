using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Sanmar
{
    class SanmarDescFr
    {
        private String manCode;
        private String scndManCode;
        private String womanCode;
        private String scndWomanCode;
        private String thrdWomanCode;
        private String youthCode;
        private String largeCode;
        private String otherCode;
        private String description;

        public SanmarDescFr()
        {
            this.manCode = "";
            this.scndManCode = "";
            this.womanCode = "";
            this.scndWomanCode = "";
            this.thrdWomanCode = "";
            this.youthCode = "";
            this.largeCode = "";
            this.otherCode = "";
            this.description = "";
        }

        public SanmarDescFr(String manCode, String scndManCode, String womanCode, String scndWomanCode, String thrdWomanCode, String youthCode, String largeCode, String otherCode, String description)
        {
            this.manCode = manCode;
            this.scndManCode = scndManCode;
            this.womanCode = womanCode;
            this.scndWomanCode = scndWomanCode;
            this.thrdWomanCode = thrdWomanCode;
            this.youthCode = youthCode;
            this.largeCode = largeCode;
            this.otherCode = otherCode;
            this.description = description;
        }

        public void setManCode(String manCode)
        {
            this.manCode = manCode;
        }

        public String getManCode()
        {
            return this.manCode;
        }

        public void setScndManCode(String scndManCode)
        {
            this.scndManCode = scndManCode ;
        }

        public String getScndManCode()
        {
            return this.scndManCode;
        }

        public void setWomanCode(String womanCode)
        {
            this.womanCode = womanCode;
        }

        public String getWomanCode()
        {
            return this.womanCode;
        }

        public void setScndWomanCode(String scndWomanCode)
        {
            this.scndWomanCode = scndWomanCode;
        }

        public String getScndWomanCode()
        {
            return this.scndWomanCode;
        }

        public void setThrdWomanCode(String thrdWomanCode)
        {
            this.thrdWomanCode = thrdWomanCode;
        }

        public String getThrdWomanCode()
        {
            return this.thrdWomanCode;
        }

        public void setYouthCode(String youthCode)
        {
            this.youthCode = youthCode;
        }

        public String getYouthCode()
        {
            return this.youthCode;
        }

        public void setLargeCode(String largeCode)
        {
            this.largeCode = largeCode;
        }

        public String getLargeCode()
        {
            return this.largeCode;
        }

        public void setOtherCode(String otherCode)
        {
            this.otherCode = otherCode;
        }

        public String getOtherCode()
        {
            return this.otherCode;
        }

        public void setDescription(String description)
        {
            this.description = description;
        }

        public String getDescription()
        {
            return this.description;
        }
    }
}
