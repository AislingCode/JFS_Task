using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JFS_Task.Pages
{
    public class IndexModel : PageModel
    {
        public FileFormat Format;
        public Period ReportPeriod;

        public IndexModel()
        {
        }

        public void OnGet()
        {

        }
    }
}