PARA O FUNCIONAMENTO O ARQUIVO bd.JSON deve estar na raiz da pasta Sistema.Gerenciameto e deve ser apontada na seguinte declaraçã onde  instancia
BaseDados<Servico> bd = new BaseDados<Servico>("D:\\WS-VSCODE\\Sistema.Gerenciameto\\bd.json");\
O pacote Newtonsoft.Json deve estar presente na solução, caso não esteja, instalar a partir do gerenciamento Manage NuGet Packages;\
Compilar a solução e executar;\
Os metodos Salvar, Atualizar e Deletar são funcionais e deve-se selecioanr um registro antes de qualquer alteração;\
O ID é gerado de forma aleatória e unica para cada registro de Serviço;\
O ID não é permito sem informado e não é permitido a sua alteração;\
O metodo Salvar funciona a partir da inserção dos dados obrigatorios, não sendo permitido salvar sem as informações. Caso haja falta de informações ele retorna uma mensagem
informando qual campo falta ser preenchido;\
Por padrão o Data grid view lista todos os serviços cadastrados, permitindo serem atualizados quando selecionar um registro;\
Após a inserçãode  um novo registro os campos são limpos, permitindo a inserção de um novo registro;
Os campos Nome, Descrição, Status e Data são alteraveis;\
O campo de busca, funciona a partir da busca pelo nome de um serviço;\
O filtro de status funciona a partir da selecção de um status no combobox;\
O filtro nenhum na seleção de status traz todos os serviços cadastrados.
