# Checkpoint 5/6

## Integrantes

- Pedro Moreira de Jesus - RM553912
- Natan Junior Rodrigues Lopes - RM552626
- Pedro Lucca Medeiros Miranda - RM553873

## Sobre a IA
A inteligência artificial deste projeto foi desenvolvida com ML.NET, utilizando algoritmos de regressão para estimar a probabilidade de ocorrência de sinistros odontológicos. O modelo é treinado a partir de dados de pacientes, levando em conta variáveis como idade, gênero, histórico odontológico, hábitos de higiene, condições médicas e tipo de plano de saúde. Com isso, a IA identifica padrões relevantes e gera uma predição personalizada do risco de sinistro para cada paciente, apoiando decisões estratégicas e a gestão eficiente de riscos em planos odontológicos.

> **Nota:** O dataset utilizado para o treinamento do modelo foi gerado aleatoriamente utilizando a biblioteca Faker em Python, simulando cenários realistas para fins de desenvolvimento e testes.

## Link do Video
https://youtu.be/JfSKCu02P78?si=sZqrkq-d0wNAsGa2

## Stack Tecnológica
* ASP.NET Core 8
* ML.NET
* Docker
* AutoMapper

## Estrutura do Projeto 
```
├── App/
│   ├── Controllers/
│   ├── DTOs/
│   └── Services/
├── Domain/
│   └── Models/
└── Infra/
    ├── Data/
    └── IA/ 
```

## Execução

### Via Docker
```bash
docker build -t checkpoint .
docker run -p 8080:8080 checkpoint
```

### Localmente
```bash
dotnet restore
dotnet run
```

## API Endpoints
| Método | Rota                | Descrição                |
|--------|---------------------|--------------------------|
| POST   | `/api/prediction/predict` | Realiza predição de risco |
| GET    | `/api/modeltraining/train` | Treina e salva o modelo   |

## Exemplo de Request

```json
POST /api/prediction
Content-Type: application/json

{
  "idade": 35,
  "genero": "Male",
  "frequenciaConsultas": 2,
  "aderenciaTratamento": 0.9,
  "historicoCaries": true,
  "doencaPeriodontal": false,
  "numeroImplantes": 1,
  "tratamentosComplexosPrevios": false,
  "fumante": true,
  "alcoolismo": true,
  "escovacaoDiaria": 2,
  "usoFioDental": true,
  "doencasSistemicas": "Diabetes",
  "medicamentosUsoContinuo": 1,
  "numeroSinistrosPrevios": 0,
  "tipoPlano": "Basico"
}
```

## Exemplo de Resposta

A resposta retorna a probabilidade prevista de ocorrência de sinistro para o paciente informado, em formato textual:

```json
{
  "Probalidade de Sinistro: 0,45096666%"
}
```
