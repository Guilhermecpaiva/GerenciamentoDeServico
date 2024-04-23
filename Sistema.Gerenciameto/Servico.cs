using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace Sistema.Gerenciameto
{
 
    //Classe que representa um serviço
    public class Servico
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public Status status { get; set; }
        public DateTime Data { get; set; }

        //construtor da classe serviço
        public Servico(int id, string titulo, string descricao, DateTime data, Status s)
        {
            Id = id;
            Titulo = titulo;
            Descricao = descricao;
            status = s;
            Data = data;
          
        }
    }
    
    //classe que representa o status de um serviço
    public class Status
    {
        public enum Tipo
        {
            Nenhum = 0,
            Execução = 1,
            Cancelado = 2,
            Aguardando = 3,
            Finalizado = 4
        }

        public Tipo tipo;
        public string Stats;

        //construtor da classe status
        public Status(string n, Tipo t) 
        {
            Stats = n;
            tipo = t;


        }
     

    }

    //classe responsavel por gerenciar a base de dados
    public class BaseDados<T>
    {
        List<T> valor = new List<T>();
        public string rota;

        //construtor da classe BaseDados
        public BaseDados(string r)
        {
            rota = r;
        }
        public List<T> ObterValor()
        {
            return valor;
        }

        //metodo para saldar a base de dados em um arquivo
        public void Salvar() 
        {

            try
            {
                string texto = JsonConvert.SerializeObject(valor);
                File.WriteAllText(rota, texto);
                MessageBox.Show("Arquivo Salvo");
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Arquivo Salvo! " );
            }
        }


        //metodo para carregar a base de dados a partir de um arquivo
        public void Carregar() 
        {
            try
            {

                string arquivo = File.ReadAllText(rota);
                valor = JsonConvert.DeserializeObject<List<T>>(arquivo);
            }
            catch (Exception ex) 
            {
                Console.WriteLine("Erro ao carregar Arquivo!" + ex.Message);
            }
        }

        //metodo para inserir um novo item na base de dados
        public void Inserir(T novo)
        {
            valor.Add(novo);         
            MessageBox.Show("Novo item inserido: " + novo.ToString());
            Salvar();
        }


        //metodo para buscar os itens na base de dados com base em um criterio
        public List<T> Buscar(Func <T, bool> criterio)
        {
            return valor.Where(criterio).ToList();
        }

        //metodo para deletar os itens da base de dados com base em um criterio
        public void Deletar(Func<T, bool> criterio)
        {
            valor = valor.Where(x => !criterio(x)).ToList();            
            MessageBox.Show("Item(s) deletado(s)");
            Salvar();
        }

        //metodo para atualizarum item na base de dados com base em um criterio
        public void Atualizar(Func<T, bool> criterio, T novo)
        {
            valor = valor.Select(x =>
            {
                if (criterio(x)) x = novo;
                return x;
            }).ToList();
            Salvar();
            Console.WriteLine("Item atualizado: " + novo.ToString());
        }

    }

}
