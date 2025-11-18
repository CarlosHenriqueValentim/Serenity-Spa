-- banco.sql completo fornecido (mantive todas as tabelas do Serenity_Spa que você mandou)
drop database if exists Serenity_Spa;
create database if not exists Serenity_Spa;
use Serenity_Spa;

create table if not exists empresa (
codigo_empresa int auto_increment not null primary key,
razao_social varchar(200) not null,
nome_fantasia varchar(200) not null,
cnpj_empresa varchar(20) not null,
localizacao_empresa varchar(200) not null,
telefone_empresa varchar(20) not null,
email_empresa varchar(100) not null,
site_empresa varchar(100) not null,
unique key uq_empresa_cnpj (cnpj_empresa),
unique key uq_empresa_email (email_empresa)
);

insert into empresa
(razao_social, nome_fantasia, cnpj_empresa, localizacao_empresa, telefone_empresa, email_empresa, site_empresa)
values
('Serenity Spa e Massagens LTDA', 'Serenity Spa', '93911881000183', 'Rua Pedro Bibiano, Jardim Barcelona, Presidente Prudente - SP', '11986288806', 'contato@serenityspa.com', 'serenityspa.com.br');

create table if not exists servicos (
codigo_servico int auto_increment not null primary key,
codigo_empresa int not null,
nome_servico varchar(100) not null,
descricao_servico text not null,
duracao_min_servico int not null,
duracao_max_servico int not null,
preco_min_servico decimal(10,2) not null,
preco_max_servico decimal(10,2) not null,
unique key uq_servicos_nome (codigo_empresa, nome_servico),
foreign key (codigo_empresa) references empresa(codigo_empresa) on delete cascade on update cascade
);

insert into servicos
(codigo_empresa, nome_servico, descricao_servico, duracao_min_servico, duracao_max_servico, preco_min_servico, preco_max_servico)
values
(1, 'Aromaterapia','Massagem com óleos essenciais para relaxamento e equilíbrio',30,60,180.00,260.00),
(1, 'Pedras Quentes', 'Massagem com pedras vulcânicas aquecidas para relaxamento muscular', 60,60,255.00,255.00),
(1, 'Shiatsu', 'Técnica oriental com pressão dos dedos em pontos específicos', 45,45,260.00,260.00),
(1, 'Com Ventosa', 'Massagem que utiliza ventosas para ativar a circulação', 30,30,125.00,125.00),
(1, 'Sueca','Massagem clássica com manobras relaxantes e terapêuticas', 45,45,210.00,210.00),
(1, 'Desportiva', 'Massagem voltada para alívio de tensões musculares em atletas', 50,80,127.50,280.00),
(1, 'Esfoliação Completa', 'Remoção de células mortas com cremes esfoliantes', 60,60,120.00,250.00),
(1, 'Envoltório de Argilas, Chocolates ou Algas', 'Tratamento nutritivo com aplicação corporal de elementos naturais', 90,90,690.00,690.00),
(1, 'Limpeza de Pele Completa','Limpeza profunda com extração de impurezas e hidratação facial', 60,60,133.00,133.00),
(1, 'Hidratação Profunda e Máscaras Faciais Nutritivas', 'Tratamento facial com aplicação de máscaras e hidratação intensa', 60,60,140.00,140.00),
(1, 'Massagem Facial', 'Massagem relaxante e estimulante para a face', 30,30,130.00,130.00),
(1, 'Sauna Seca ou a Vapor', 'Acesso à sauna seca ou úmida para relaxamento', 30,30,50.00,80.00),
(1, 'Banho com Sais de Banho', 'Imersão relaxante com sais especiais', 30,30,100.00,150.00);

create table if not exists pacotes (
codigo_pacote int auto_increment primary key,
codigo_empresa int not null,
nome_pacote varchar(100) not null,
descricao_pacote text not null,
preco_pacote decimal(10,2) not null,
duracao_pacote int not null,
valor_por_pessoa_extra_pacote decimal(10,2) not null default 0.00,
foreign key (codigo_empresa) references empresa(codigo_empresa) on delete cascade on update cascade
);

insert into pacotes
(codigo_empresa, nome_pacote, descricao_pacote, preco_pacote, duracao_pacote, valor_por_pessoa_extra_pacote)
values
(1, 'Spa Day Dois ou Mais', 'Escalda pés aromático, hidratação profunda, massagem facial, massagem relaxante corporal, shiatsu', 813.00, 180, 150.00),
(1, 'Spa Day Individual', 'Ritual de recepção, esfoliação corporal com lama quente e óleos essenciais, escalda pés aromático, massagem corporal com pedras quentes, hidratação profunda, massagem facial relaxante', 713.00, 240, 0.01);

create table if not exists itens_pacote (
codigo_itens_pacote int auto_increment primary key,
codigo_pacote int not null,
codigo_servico int not null,
ordem_execucao_itens_pacote int not null,
quantidade_itens_pacote int not null,
foreign key (codigo_pacote) references pacotes(codigo_pacote) on delete cascade on update cascade,
foreign key (codigo_servico) references servicos(codigo_servico) on delete restrict on update cascade
);

insert into itens_pacote
(codigo_pacote, codigo_servico, ordem_execucao_itens_pacote, quantidade_itens_pacote)
values
(1, 13, 1, 1),
(1, 10, 2, 1),
(1, 11, 3, 1),
(1, 5, 4, 1),
(1, 3, 5, 1),
(2, 7, 1, 1),
(2, 13, 2, 1),
(2, 2, 3, 1),
(2, 10, 4, 1),
(2, 11, 5, 1);

create table if not exists produtos_estoque (
codigo_produto int auto_increment not null primary key,
codigo_empresa int not null,
nome_produto_estoque varchar(100) not null,
categoria_produto_estoque varchar(50) not null,
quantidade_produto_estoque int not null,
preco_unitario_produto_estoque decimal(10,2) not null,
preco_total_produto_estoque decimal(10,2) not null,
fornecedor_produto_estoque varchar(100) not null,
foreign key (codigo_empresa) references empresa(codigo_empresa) on delete cascade on update cascade
);

insert into produtos_estoque
(codigo_empresa, nome_produto_estoque, categoria_produto_estoque, quantidade_produto_estoque, preco_unitario_produto_estoque, preco_total_produto_estoque, fornecedor_produto_estoque)
values
(1, 'Maca dobrável', 'Equipamento', 1, 1310.00, 1310.00, 'Andrey'),
(1, 'Kit bolsa térmica com pedras quentes', 'Equipamento', 1, 163.33, 163.33, 'Mercado Livre'),
(1, 'Kit 12 ventosas', 'Acessório', 1, 47.99, 47.99, 'Mercado Livre'),
(1, 'Álcool 70% 500ml', 'Limpeza', 2, 9.28, 18.56, 'Magazine Luiza'),
(1, 'Água sanitária 1L', 'Limpeza', 2, 5.20, 10.40, 'Amazon'),
(1, 'Máscara descartável (100 unid)', 'Proteção', 1, 38.51, 38.51, 'Magazine Luiza'),
(1, 'Aquecedor de pedras quentes', 'Equipamento', 1, 385.90, 385.90, 'Amazon'),
(1, 'Vela aromática maracujá com baunilha', 'Aromatizante', 5, 21.90, 109.50, 'Yalume'),
(1, 'Sais de banho esfoliante Romance', 'Cosmético', 2, 89.00, 178.00, 'Feito Brasil'),
(1, 'CeraVe Loção Hidratante Corporal', 'Cosmético', 2, 88.60, 177.20, 'Amazon'),
(1, 'Loção Nativa Spa Ameixa Negra', 'Cosmético', 1, 63.00, 63.00, 'Amazon'),
(1, 'Toalha Banho Hotel 100% Algodão', 'Toalhas', 2, 157.90, 315.80, 'Amazon'),
(1, 'Roupão Cortex Microfibra', 'Vestuário', 8, 56.31, 450.48, 'Amazon'),
(1, 'Óleo de Massagem Vegetal', 'Massagem', 2, 59.90, 119.80, 'Kalya Tantra'),
(1, 'Argilas corporais e faciais', 'Cosmético', 3, 33.00, 99.00, 'Mercado Livre'),
(1, 'Kit máscaras nutritivas', 'Cosmético', 1, 188.73, 188.73, 'Mercado Livre'),
(1, 'Creme Facial Creamy Calming', 'Cosmético', 2, 46.90, 93.80, 'Gendo ERP');

create table if not exists financeiro (
codigo_financeiro int auto_increment not null primary key,
codigo_empresa int not null,
tipo_financeiro enum('receita', 'despesa') not null,
descricao_financeiro text not null,
valor_financeiro decimal(10,2) not null,
data_financeiro date not null,
foreign key (codigo_empresa) references empresa(codigo_empresa) on delete cascade on update cascade
);

insert into financeiro(codigo_empresa, tipo_financeiro, descricao_financeiro, valor_financeiro, data_financeiro)
values
(1, 'despesa','Aluguel do espaço comercial',1200.00,'2025-05-01'),
(1, 'despesa','Conta de energia elétrica',1400.00,'2025-05-01'),
(1, 'despesa','Conta de água',220.00,'2025-05-01');

create table if not exists funcionarios (
codigo_funcionario int auto_increment not null primary key,
codigo_empresa int not null,
nome_funcionario varchar(100) not null,
cargo_funcionario varchar(50) not null,
telefone_funcionario varchar(15) not null,
email_funcionario varchar(100) not null,
foreign key (codigo_empresa) references empresa(codigo_empresa) on delete cascade on update cascade,
unique key uq_funcionarios_email (email_funcionario)
);

insert into funcionarios(codigo_empresa, nome_funcionario, cargo_funcionario, telefone_funcionario, email_funcionario)
values
(1, 'Eduarda da Rocha','Massagista','1193247875','eduarda.rocha@gmail.com'),
(1, 'Heloisa Marques Pereira','Gerente','11957888841','marques.heloisa@gmail.com'),
(1, 'Mariana Yukimi Ishigaki','Sócia e Recepcionista','11923479675','mariana.yukimi@gmail.com');

create table if not exists clientes (
codigo_cliente int auto_increment not null primary key,
codigo_empresa int not null,
nome_cliente varchar(200) not null,
telefone_cliente varchar(20) not null,
email_cliente varchar(200) not null,
nascimento_cliente date not null,
sexo_cliente char(1) not null check (sexo_cliente in ('m','f')),
foreign key (codigo_empresa) references empresa(codigo_empresa) on delete cascade on update cascade,
unique key uq_clientes_email (email_cliente)
);

insert into clientes(codigo_empresa, nome_cliente, telefone_cliente, email_cliente, nascimento_cliente, sexo_cliente)
values
(1, 'Mirela Silva Aparecida','11962268712','nascil.rela@gmail.com','1998-08-03','f');

create table if not exists agendamentos (
codigo_agendamento int auto_increment not null primary key,
codigo_empresa int not null,
codigo_cliente int not null,
codigo_funcionario int not null,
codigo_servico int not null,
data date not null,
duracao_agendamento time,
status enum('agendado','concluido','cancelado') not null,
foreign key (codigo_empresa) references empresa(codigo_empresa) on delete cascade on update cascade,
foreign key (codigo_cliente) references clientes(codigo_cliente) on delete restrict on update cascade,
foreign key (codigo_funcionario) references funcionarios(codigo_funcionario) on delete restrict on update cascade,
foreign key (codigo_servico) references servicos(codigo_servico) on delete restrict on update cascade
);

-- adiciono tabela agendamentos_simples para formulários diretos
create table if not exists agendamentos_simples (
  id int auto_increment primary key,
  nome varchar(100) not null,
  telefone varchar(30) not null,
  servico varchar(100) not null,
  data date not null,
  hora time not null,
  obs text,
  criado_em datetime default now()
);

-- também incluo a tabela usuario e agenda do "Projeto Finalizado" (para compatibilidade)
create table if not exists usuario(
	id_user int auto_increment,
    nome_user varchar(100) not null,
    rg_user char(9) unique,
    data_nasc_user date,
    data_cad_user datetime,
    login_user varchar(100) unique not null,
    senha_user varchar(100) not null,
    primary key(id_user)
);

create table if not exists agenda(
	id_ag int auto_increment,
    id_user int not null,
    desc_ag varchar(255) not null,
    nome_ag varchar(50) not null,
    data_ini_ag date,
    dia_ag int not null default 1,
    primary key(id_ag),
    foreign key(id_user) references usuario(id_user)
	on delete cascade
);

-- Insere usuário e agenda de exemplo do Projeto Finalizado
insert into usuario(nome_user,rg_user,data_nasc_user,data_cad_user,login_user,senha_user)values
('Josney Almeida','663847364','1991-04-23', now(),'jojo_bizarre','jf45T&'),
('Fernanda Helena Pereira','443948273','1997-08-18',now(),'fefe_metal','jdur493');

insert into agenda(id_user,desc_ag,nome_ag,data_ini_ag,dia_ag)values
(1,'Trocar a bateria do carro','bateria','2025-10-22',1),
(2,'Ensaio com a banda','ensaio','2025-10-29',1);
