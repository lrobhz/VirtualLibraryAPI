# VirtualLibraryAPI

Este é um projeto .NET Core Web Api utilizado para demonstrar boas práticas para criação de APIs Rest.

As orientações e exemplos práticos no projetos encontram-se abaixo:

1. **Organize APIs ao longo de recursos.** As URIs expostos por um serviço REST devem ser baseados em
substantivos (os dados aos quais a API da Web fornece acesso) e não em verbos (o que uma aplicação pode
fazer com os dados). <br />
**Aplicação no projeto:** api/v1/**books**/
                          api/v1/**books**/1/review/

2. Padronize as suas APIs. Adote uma convenção de nomenclatura consistente nas URIs. Em geral, é útil usar
substantivos no plural para URIs que fazem referência a coleções. <br />
**Aplicação no projeto:** api/v1/**books**/
                          api/v1/**books**/1/review/

3. Evite APIs anêmicas. Evite projetar uma interface REST que espelhe ou dependa da estrutura interna dos
dados que ela expõe. O REST é mais do que implementar operações CRUD simples (Create, Retrieve, Update,
Delete) sobre tabelas separadas em um banco de dados relacional. O objetivo do REST é mapear as entidades
de negócios e as operações que um aplicativo pode executar nessas entidades para a implementação física dessas entidades, mas um cliente não deve ser exposto a esses detalhes físicos. <br />
**Aplicação no projeto:** api/v1/basket/{userName}/RemoveBook/{bookId}
                          api/v1/orders/{orderId}/status

4. Crie APIs simples. Evite criar URIs de recursos mais complexos do que coleção/item/coleção. <br />
**Aplicação no projeto:**  api/v1/basket/{userName}/RemoveBook/{bookId}

5. Considere a atualização em lote para operações complexas. Considere implementar operações HTTP PUT
em massa que possam atualizar em lotes vários recursos de uma coleção de dados. A solicitação PUT deve
especificar o URI da coleção e o corpo de solicitação deve especificar os detalhes dos recursos a serem
modificados. Esta abordagem pode ajudar a reduzir ineficiências do protocolo HTTP e melhorar o desempenho
da sua aplicação.<br />
**Aplicação no projeto:** N/A

6. Se você precisar receber datas e horas nas API, use o padrão ISO 8601. APIs que trafegam datas ou horas
devem usar o padrão ISO 8601 para garantir interoperabilidade - https://www.w3.org/TR/NOTE-datetime. <br />
**Aplicação no projeto:**  
            ```C#
            [JsonConverter(typeof(CustomDateTimeConverter))]
            public DateTime OrderDate { get; set; }
            ```
7. Documente sua API. Uma API é tão boa quanto sua documentação. Os documentos devem ser fáceis de
encontrar e publicamente acessíveis. A maioria dos desenvolvedores verificará os documentos antes de tentar
qualquer esforço de integração. Quando os documentos estão ocultos, ausentes ou obscuros, qualquer esforço
de integração será muito aumentado. <br />
**Aplicação no projeto:**
            .../swagger/index.html

8. Use protocolo HTTPS/SSL. Sempre! Suas APIs da web podem ser acessadas de qualquer lugar onde haja
internet (como bibliotecas, cafés, aeroportos, entre outros). Nem todos estes são seguros. Muitos não
criptografam as comunicações, permitindo fácil escutas ou falsificação de identidade se as credenciais de
autenticação forem sequestradas. Outra vantagem de sempre usar o SSL é que as comunicações criptografadas
garantidas simplificam os esforços de autenticação - você pode usar tokens de acesso simples em vez de assinar cada solicitação da API. <br />
**Aplicação no projeto:**  N/A

9. Versione suas APIs. O versionamento permite que você possa evoluir suas APIs sem quebrar o
funcionamento de seus clientes. Se você examinar cada URL da API da Marvel verá que a informação de versão
foi disponibilizada. <br />
**Aplicação no projeto:**  ../Api/**v1**/books/

10. Estabeleça paginação para coleções com grandes volumes de dados. Não faz sentido retornar 20.000
clientes em um único pacote JSON. E para evitar grandes pacotes de dados devemos estabelecer controles de
paginações. <br />
**Aplicação no projeto:**
        ```
        public class Pagination
        {
            public int PageNumber { get; set; }
            public int PageSize { get; set; }
            public int? TotalRecords { get; set; }
            public int? TotalPages { get; set; }
        } ```

11. Use corretamente os códigos de retorno HTTP. Por exemplo, se você está implementado um GET e o
processamento foi bem-sucedido o retorno deve ser 200. Se você está implementado um POST e o
processamento for bem-sucedido você deve devolver um código 201 ou 202. Se você violar essas regras, a sua
API não será RESTful e seus clientes poderão ter problemas diversos ao usá-la. Caso o código seja de erro
(4XX ou 5XX), o corpo da mensagem deveria conter informações adicionais sobre o problema com o pedido
e os formatos esperados, ou conter um link para um URL que fornece mais detalhes.<br />
**Aplicação no projeto:** 
    ```
     return Conflict(new { message = $"The user {userName} has no basket."});
    ...
     return Ok();
    ...
    return NoContent();