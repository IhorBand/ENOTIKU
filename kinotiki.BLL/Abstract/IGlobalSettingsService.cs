using kinotiki.BLL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kinotiki.BLL.Abstract
{
    public interface IGlobalSettingsService
    {
        GlobalSettings Get();
        void Save(GlobalSettings newSettings);
    }
}
