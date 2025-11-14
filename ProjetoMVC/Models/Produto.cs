namespace ProjetoMVC.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public  string Nome { get; set; }
        public  string Categoria { get; set; }
        public decimal Preco { get; set; }

        public int FornecedorId { get; set; }

        public Fornecedor Fornecedor { get; set; } // Classe(tipo) e propriedade(dados que trazem lá do banco no join
    }
}
