## Mensageria

1. Mensageria é uma maneira de permitir a comunicação entre diferentes sistemas ou componentes de software de forma desacoplada.
2. Comunicação desacoplada significa que os sistemas não precisam saber uns dos outros para se comunicar.
3. A mensageria é baseada em mensagens, que são pacotes de dados que contêm informações e instruções.
4. Em vez de se comunicarem diretamente uns com os outros, os sistemas enviam mensagens para uma fila de mensagens, que é como se fosse uma caixa de correio.
5. Benefícios da mensageria:
    - Permite que sistemas distribuídos sejam desenvolvidos, implantados, e escalados de forma independente.
    - Melhora a resiliência do sistema, pois uma falha em um componente não afeta diretamente os outros.
    - Melhora a performance em fluxo de operações com múltiplos passos, pois permite que nem todos precisem ser feitos na requisição inicial.

## RabbitMQ

1. É uma ferramenta de mensageria open-source bastante popular.
2. Possui suporte a vários protocolos de mensageria e que pode ser facilmente conectado a diversos sistemas, incluindo aplicações .NET.
3. É conhecido por ser leve e fácil de publicar tanto on premise quanto na nuvem.
4. Também suporta requisitos de alta escalabilidade e disponibilidade.
5. Em uma comunicação através de mensageria, o RabbitMQ atua como um intermediário na troca de mensagens entre os produtores e consumidores.
6. Os principais conceitos relacionados ao RabbitMQ são:
    - Exchanges
    - Filas
    - Bindings
    - Routing Keys
7. O RabbitMQ oferece diversas funcionalidades, como:
    - Roteamento flexível de mensagens
    - Dead Letter Queue
    - Confirmação (acknowledgement) de mensagens
    - Clusterização para alta disponibilidade
    - API HTTP, interface de usuário e linha de comando para gerenciamento e monitoramento
    - Plugins

## Arquitetura do RabbitMQ

1. **Fila**:
    - É um armazenamento temporário de mensagens que ainda não foram processadas.
    - As mensagens são publicadas para os Exchanges, que então as redirecionam para a fila correspondente.
    - Os consumidores recebem mensagens das filas, processando as suas informações.
    - Ao criar uma fila há várias propriedades que podem ser configuradas:
        - **Durabilidade**: Se for true, significa que elas sobreviverão a um reinício do RabbitMQ e as mensagens não confirmadas serão re-enfileiradas.
        - **Exclusividade**: Se for true, significa que a fila será exclusiva, sendo usada por apenas uma conexão e então excluída quando essa conexão for fechada.
        - **Auto-delete**: Se for true, ela será automaticamente apagada quando o último consumidor se desconectar.
        - **Message TTL**: Se refere ao período de tempo que uma mensagem não entregue pode permanecer na fila antes de ser descartada ou movida para outra fila. O valor é especificado em milissegundos.
2. **Exchanges**:
    - São um componente essencial na arquitetura do RabbitMQ.
    - Eles recebem mensagens dos produtores e as direcionam para as filas.
    - Este direcionamento é baseado em regras denominadas bindings, que são estabelecidas entre os Exchanges e as filas.
    - Os bindings podem avaliar as Routing Keys, que seria um atributo adicionado ao cabeçalho da mensagem, que serve como um endereço que o exchange poderá decidir como rotear a mensagem ao definir o binding.
    - Existem 4 tipos de Exchange:
        - **Direct**: Envia mensagens para uma ou mais filas que os bindings exatamente encaixam com a routing key.
        - **Default**: É uma Exchange de tipo "Direct", sem nome "", que todas filas criadas se registram com a routing key igual ao seu nome.
        - **Topic**: Similar ao Direct, mas oferece mais flexibilidade pois permite utilizar padrões de correspondência com asterisco (*) e hash (#), não necessitando ter a binding key igual a routing key.
            - **Asterisco (*)**: representa uma única palavra qualquer.
            - **Hash (#)**: representa qualquer número de palavras.
        - **Fanout**: Copia e roteia todas mensagens recebidas para todas filas que estão registradas na Exchange, independentemente de routing key ou padrões definidos.

## RabbitMQ.Client

1. É a biblioteca oficial do RabbitMQ para .NET.
2. Seu principal benefício é que ela fornece controle completo sobre o RabbitMQ, oferecendo acesso direto a todos os recursos do RabbitMQ sem qualquer abstração.
3. Principais recursos:
    - Controle completo
    - Performance
        - Tende a oferecer melhor desempenho em comparação com outras bibliotecas de mais alto nível como o EasyNetQ.
    - Suporte para todos os protocolos RabbitMQ
        - Suporta todos os protocolos RabbitMQ, incluindo AMQP 0-9-1, AMQP 1.0, STOMP e MQTT.
    - Manutenção pela equipe do RabbitMQ

## EasyNetQ

1. É uma biblioteca open-source para o RabbitMQ.
2. A principal característica do EasyNetQ é a simplicidade de interação com o RabbitMQ, fornecendo uma interface de alto nível e fácil de usar.
3. Com isso, ele esconde muitos dos detalhes de implementação que podem tornar o RabbitMQ difícil de gerenciar.
4. Porém, é essencial entender que não se deve utilizar esta característica como desculpa para não entender como o RabbitMQ funciona.
    - Isso pode resultar em problemas difíceis de rastrear e corrigir.
5. Principais recursos:
    - Interface de alto nível: ao oferecer abstrações e convenções em cima do RabbitMQ, o EasyNetQ é simples de se utilizar.
    - Serialização e desserialização de mensagens: Realiza a serialização e desserialização de mensagens automaticamente.
    - Reconexão de consumidor: detecta problemas de conexão e reinicia consumidores.
    - Gerenciamento de erros: lida de forma automática com uma variedade de erros, incluindo falhas de conexão e mensagens mal-formadas.

## Padrão Pub-Sub

1. O padrão Publish-Subscribe é um padrão de projeto de software que permite que componentes enviem mensagens uns aos outros sem precisar conhecer diretamente quem as receberá.
2. Em vez disso, os componentes simplesmente publicam suas mensagens em um tópico, e outros se inscrevem para receber notificações sobre esses tópicos.
3. Ele é útil em situações em que há dois ou mais objetos que podem estar interessados em receber notificações sobre determinados eventos ou atualizações de estado em seu sistema.
4. Ele permite que esses componentes se inscrevam para receber apenas as notificações que lhes interessam, ao invés de ter que receber todas as atualizações enviadas por um determinado Componente.
5. Além disso, o padrão Publish-Subscribe ajuda a evitar dependências fortes entre os objetos, pois eles não precisam conhecer diretamente uns aos outros, resultando em menor acoplamento entre os componentes do sistema.
6. O padrão Pub-Sub é implementado com a utilização de um mediador, que é responsável por gerenciar a publicação de mensagens e a inscrição de objetos para receber notificações.
7. Ele pode ser implementado de várias maneiras, como por exemplo, utilização de um Message Broker como o RabbitMQ.