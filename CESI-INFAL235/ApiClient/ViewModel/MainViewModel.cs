using ApiClient.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiClient.ViewModel
{
    public class MainViewModel
    {
        public ApiAccess ApiAccess { get; set; }

        public MainViewModel(MainWindow window)
        {
            ApiAccess = Api.ApiAccess.GetInstance(window);
        }

        public async Task<string> GetDate()
        {
            string result = await ApiAccess.GetRoute(Properties.Routes.Default.Date);
            if (string.IsNullOrWhiteSpace(result))
            {
                return "error";
            }
            else
            {
                return result;
            }
        }
    }
}
