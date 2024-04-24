namespace Sistema.Gerenciamento.Teste

open Microsoft.VisualStudio.TestTools.UnitTesting
open Sistema.Gerenciameto
open System
open System.Linq
open System.Windows

module Tests =

    let ArquivoTeste = "D:\\WS-VSCODE\\Sistema.Gerenciameto\\bd_teste.json"
    let mutable bd = BaseDados<Servico>(ArquivoTeste)

    [<TestClass>]
    type Tests() =
        member this.Dummy() = ()

        [<TestMethod>]
        member this.Inserir_Teste() =        
            bd.Carregar()
            let totalA = bd.ObterValor().Count
            let novo = new Servico (1234, "Teste", "Inserindo via teste", DateTime.Now, new Status("Teste", Status.Tipo.Nenhum))

            bd.Inserir(novo)

            let totalD = bd.ObterValor().Count
            Assert.AreEqual(totalA + 1, totalD)           
            Assert.IsTrue(bd.ObterValor().Contains(novo))

        [<TestMethod>]
        member this.Deletar_Teste() =        
            bd.Carregar()
            let totalA = bd.ObterValor().Count
            let novo = new Servico (1234, "Teste", "Deletando via teste", DateTime.Now, new Status("Teste", Status.Tipo.Nenhum))

            bd.Deletar(fun x -> x.Id = 1234)

            let totalD = bd.ObterValor().Count
            Assert.AreEqual(totalA - 1, totalD)
            Assert.IsFalse(bd.ObterValor().Contains(novo))

        [<TestMethod>]
        member this.Atualizar_Teste() =        
            bd.Carregar()
            let totalA = bd.ObterValor().Count
            let novo = new Servico (1234, "Teste", "Atualizando via teste", DateTime.Now, new Status("Teste", Status.Tipo.Nenhum))
            bd.Inserir(novo);            
            let atualizado = new Servico (1234, "Teste Atualizado", "Atualizado via teste", DateTime.Now, new Status("Teste", Status.Tipo.Nenhum))

            bd.Atualizar((fun x -> x.Id = 1234), atualizado)


            let itemA = bd.ObterValor().FirstOrDefault (fun x -> x.Id = 1234)
            Assert.IsNotNull(itemA)
            Assert.AreEqual(atualizado.Titulo, itemA.Titulo)
            Assert.AreEqual(atualizado.Descricao, itemA.Descricao)
