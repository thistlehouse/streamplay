# 🎬 StreamPlay

**StreamPlay** é uma plataforma de **streaming interativo simplificado**, com foco em **modularidade** e **arquitetura limpa**, construída em **.NET 9**.

O projeto tem como objetivo permitir interações em tempo real entre criadores de conteúdo e espectadores, integrando **chat**, **notificações**, **wallet (moeda virtual)** e gerenciamento de **streams** de forma extensível.

---

## 🚀 Tecnologias Utilizadas

- **.NET 9** – Backend modular
- **XUnit** – Testes unitários
- **MediatR** – Comunicação interna entre casos de uso (CQRS)
- **Entity Framework Core** (planejado) – Persistência
- **OpenTelemetry** (planejado) – Observabilidade
- **Kafka / Event Bus** (planejado) – Integração entre módulos
- **Docker & Kubernetes** (planejado) – Deploy escalável

---

## 📌 Principais Módulos

- **Users** – Cadastro, autenticação e gerenciamento de perfis
- **Streams** – Criação e controle de transmissões ao vivo
- **Chat** – Canal de comunicação em tempo real
- **Notifications** – Envio de alertas e updates de eventos
- **Wallets** – Sistema de créditos e pagamentos internos

---

## 🎯 Roadmap

- [x] Estrutura inicial do projeto (modular monolito)
- [x] Módulo **Users** (User, UserRepository, RegisterUserUseCase)
- [ ] API inicial com **Minimal API / Controllers**
- [ ] Persistência com EF Core
- [ ] Observabilidade com OpenTelemetry
- [ ] Integração entre módulos via eventos
- [ ] Deploy containerizado (Docker / Kubernetes)
