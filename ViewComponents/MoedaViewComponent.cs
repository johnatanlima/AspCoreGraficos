using System.Linq;
using System.Threading.Tasks;
using AspCoreGraficos.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspCoreGraficos.ViewComponents
{
    public class MoedaViewComponent : ViewComponent
    {
        private readonly MoedaDbContexto _contexto;

        public MoedaViewComponent(MoedaDbContexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            return View(await _contexto.Moedas.ToListAsync()); 
        }

    }
}