using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Windows.Forms;       

namespace Sistema.Gerenciameto
{
    public partial class Form1 : Form

    {
        //Declaração de uma instancia da classe Base de dados que trabalha com a classe serviços
        BaseDados<Servico> bd = new BaseDados<Servico>("D:\\WS-VSCODE\\Sistema.Gerenciameto\\bd.json");

        //Metodo que exibe os dados de uma lista de serviço no Datagridview
        //Mostra os serviços encontrados no datagridview
        //busca servicos com o titulo digitado
        void mostrar(List<Servico> lista)
        {
            dgvListar.Rows.Clear();

            foreach (Servico p in lista)
            {

                DataGridViewRow dataGridView = new DataGridViewRow();
                dataGridView.Cells.Add(new DataGridViewTextBoxCell() { Value = p.Id });
                dataGridView.Cells.Add(new DataGridViewTextBoxCell() { Value = p.Titulo });
                dataGridView.Cells.Add(new DataGridViewTextBoxCell() { Value = p.Descricao });
                dataGridView.Cells.Add(new DataGridViewTextBoxCell() { Value = p.status.tipo });
                dataGridView.Cells.Add(new DataGridViewTextBoxCell() { Value = p.Data.ToString("dd/MM/yyyy") });
                dgvListar.Rows.Add(dataGridView);
            }
        }


        //Construtor da classe 
        public Form1()
        {
            InitializeComponent();
            cbFiltro.SelectedIndex = 0;
            bd.Carregar();
            mostrar(bd.ObterValor());
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblData_Click(object sender, EventArgs e)
        {

        }

        
        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            //Atualiza o Serviço com os dados inseridos
            int Id = int.Parse(txtId.Text);
            Status.Tipo t;

            switch (cbStatus.SelectedIndex)
            {

                default: t = Status.Tipo.Nenhum; break;
                case 1: t = Status.Tipo.Execução; break;
                case 2: t = Status.Tipo.Cancelado; break;
                case 3: t = Status.Tipo.Aguardando; break;
                case 4: t = Status.Tipo.Finalizado; break;
            }

            Status s = new Status(txtServico.Text, t);
            Servico p = new Servico(Id, txtServico.Text, txtDescricao.Text, dtTime.Value, s);
            bd.Atualizar(x => x.Id == Id, p);
            mostrar(bd.ObterValor());
            LimparCampos();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            BotaoAtualizar(false);
            BotaoDeletar(false);
        }

        //Evento de clique no botão salvar
        //verifica se os campos estão preenchidos corretamente e interrompe caso não
        private void btnSalvar_Click(object sender, EventArgs e)
        {

            if(!ValidarCampos())
            {
                return;
            }

            Status.Tipo t;
            switch (cbStatus.SelectedIndex)
            {

                default: t = Status.Tipo.Nenhum; break;
                case 1: t = Status.Tipo.Execução; break;
                case 2: t = Status.Tipo.Cancelado; break;
                case 3: t = Status.Tipo.Aguardando; break;
                case 4: t = Status.Tipo.Finalizado; break;
            }

            //Cria um novo objeto 
            //Cria um objeto Servico com ID aleatorio entre 1000 e 9999
            Status s = new Status(txtServico.Text, t);
            Servico p = new Servico((new Random()).Next(1000, 9999), txtServico.Text, txtDescricao.Text, dtTime.Value, s);
            bd.Inserir(p);
            mostrar(bd.ObterValor());
            LimparCampos();
        }

        //Evento de clique onde deleta o serviço com ID especificado 
        //Mostra todos os Serviços atualizados
        //Limpa os campos do formulario
        private void btnDeletar_Click(object sender, EventArgs e)
        {
            int Id = int.Parse(txtId.Text);
            bd.Deletar(x => x.Id == Id);
            mostrar(bd.ObterValor());
            LimparCampos();

        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            var lista = bd.Buscar(x => x.Titulo.Contains(txtBuscar.Text));
            mostrar(lista);

        }

        //Evento de clique na celula do DataGridView
        //habilita o botão de Atualizar e botão de Deletar
        private void dgvListar_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            BotaoAtualizar(true);
            BotaoDeletar(true);
            Servico p = bd.Buscar(x => x.Id.ToString() == dgvListar.CurrentRow.Cells[0].Value.ToString())[0];
            txtId.Text = p.Id.ToString();
            txtServico.Text = p.Titulo;
            txtDescricao.Text = p.Descricao;
            cbStatus.SelectedIndex = (int)p.status.tipo;
        }

        //Evento de mudança no indice do Combobox de status
        private void cbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbStatus.Focused)
            {
                filtroSelecionado = true;
                FiltroStatus();
            }
            
        }

        private void label1_Click_2(object sender, EventArgs e)
        {

        }


        //método para validar os campos do formulario
        private bool ValidarCampos()
        {

            if (string.IsNullOrWhiteSpace(txtServico.Text))
            {
                MessageBox.Show("Campo de Serviço é obrigatorio");
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtDescricao.Text))
            {
                MessageBox.Show("Campo de Descrição é obrigatório");
                return false;
            }
            return true;
        }

        private void BotaoAtualizar(bool habilitar)
        {
            btnAtualizar.Enabled = habilitar;
        }

        private void BotaoDeletar(bool habilitar)
        {
            btnDeletar.Enabled = habilitar;
        }


        //metodo para aplicar filtro de status selecionado
        private void FiltroStatus()
        {
            if (filtroSelecionado)
            {
                var statusSelecionado = (Status.Tipo)cbFiltro.SelectedIndex;
                var listaFiltrada = bd.ObterValor().Where(x => x.status.tipo == statusSelecionado).ToList();
                mostrar(listaFiltrada);
            }
        }

        private bool filtroSelecionado = false;

        //Mudança no indice selecionado no combobox de filtro
        //Mostra todos os serviços no Datagridview
        //Filtra os serviços pelo status selecionado
        private void cbFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            filtroSelecionado = false;
            if (cbFiltro.Focused)
            {
                if(cbFiltro.SelectedIndex == 0)
                {
                    filtroSelecionado = false;
                    mostrar(bd.ObterValor());
                }

                else
                {
                    filtroSelecionado = true;
                    FiltroStatus();
                }
            }
        }

        //metodo que limpa os campos
        private void LimparCampos()
        {
            txtServico.Text = "";
            txtDescricao.Text = "";
            cbStatus.SelectedIndex = 0;
            dtTime.Value = DateTime.Now;
        }
    }
}