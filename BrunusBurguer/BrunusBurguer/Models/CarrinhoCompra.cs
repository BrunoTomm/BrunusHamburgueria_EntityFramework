using BrunusBurguer.Context;
using Microsoft.EntityFrameworkCore;

namespace BrunusBurguer.Models
{
    public class CarrinhoCompra
    {
        private readonly AppDbContext _context;

        public CarrinhoCompra(AppDbContext context)
        {
            _context = context;
        }

        public string CarrinhoCompraId { get; set; }
        public List<CarrinhoCompraItem> CarrinhoCompraItens { get; set; }


        public static CarrinhoCompra GetCarrinho(IServiceProvider services)
        {
            //Define uma sessao
            ISession session =
                        services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session; //Se a instancia de IHttpContextAccessor nao for null, ele irá retornar uma sessao

            //obtem um servico do tipo do nosso contexto
            var context = services.GetService<AppDbContext>();

            //obtem ou gera o Id do carrinho
            string carrinhoId = session.GetString("CarrinhoId") ?? Guid.NewGuid().ToString(); //tenta recuperar um valor de carrinho da session, se for null, atribui um novo gui para o carrinhoId

            //com o valor acima obtido, atribui o id do carrinho na session
            session.SetString("CarrinhoId", carrinhoId);

            //retorna o carrinho com contexto e o Id atribuido ou obtido
            return new CarrinhoCompra(context)
            {
                CarrinhoCompraId = carrinhoId,
            };
        }

        public void AdicionarAoCarrinho(Lanche lanche)
        {
            var carrinhoCompraItem = _context.CarrinhoCompraItens.SingleOrDefault(
                        x => x.Lanche.LancheId == lanche.LancheId &&
                        x.CarrinhoCompraId == CarrinhoCompraId);

            if (carrinhoCompraItem == null) //quer dizer que nn existe no carrinho
            {
                carrinhoCompraItem = new CarrinhoCompraItem()
                {
                    CarrinhoCompraId = CarrinhoCompraId,
                    Lanche = lanche,
                    Quantidade = 1
                };

                _context.CarrinhoCompraItens.Add(carrinhoCompraItem);
            }
            else
            {
                carrinhoCompraItem.Quantidade++;
            }
            _context.SaveChanges();
        }

        public void RemoverDoCarrinho(Lanche lanche)
        {
            var carrinhoCompraItem = _context.CarrinhoCompraItens.SingleOrDefault(
                        x => x.Lanche.LancheId == lanche.LancheId &&
                        x.CarrinhoCompraId == CarrinhoCompraId);

            if (carrinhoCompraItem != null)
            {
                if (carrinhoCompraItem.Quantidade > 1)
                {
                    carrinhoCompraItem.Quantidade--;
                }
                else
                {
                    _context.CarrinhoCompraItens.Remove(carrinhoCompraItem);
                }
            }
            _context.SaveChanges();
        }

        public List<CarrinhoCompraItem> GetCarrinhoCompraItens()
        {
            return CarrinhoCompraItens ?? (CarrinhoCompraItens = _context.CarrinhoCompraItens
                                                                    .Where(c => c.CarrinhoCompraId == CarrinhoCompraId)
                                                                    .Include(l => l.Lanche)
                                                                    .ToList());
        }

        public void LimparCarrinho()
        {
            var carrinhoItens = _context.CarrinhoCompraItens.Where(carrinho => carrinho.CarrinhoCompraId == CarrinhoCompraId);

            _context.CarrinhoCompraItens.RemoveRange(carrinhoItens);
            _context.SaveChanges();
        }

        public decimal GetCarrinhoCompraTotal()
        {
            var total = _context.CarrinhoCompraItens.Where(c => c.CarrinhoCompraId == CarrinhoCompraId)
                                                    .Select(l => l.Lanche.Preco * l.Quantidade)
                                                    .Sum();
            return total;
        }
    }
}
