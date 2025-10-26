create database if not exists serenityspa;
use serenityspa;

create table if not exists empresa (
codigo_empresa int auto_increment not null primary key,
razao_social varchar(200) not null,
nome_fantasia varchar(200) not null,
cnpj_empresa varchar(20) not null,
localizacao_empresa varchar(200) not null,
telefone_empresa varchar(20) not null,
email_empresa varchar(100) not null,
site_empresa varchar(100) not null
);

insert into empresa 
(razao_social, nome_fantasia, cnpj_empresa, localizacao_empresa, telefone_empresa, email_empresa, site_empresa)
values 
('serenity spa e massagens ltda', 'serenity spa', '93911881000183', 'rua pedro bibiano, jardim barcelona, presidente prudente - sp', '11986288806', 'contato@serenityspa.com', 'serenityspa.com.br');

create table if not exists servicos (
codigo_servico int auto_increment not null primary key,
codigo_empresa int not null,
nome_servico varchar(100) not null unique,
descricao_servico text not null,
duracao_min_servico int not null,
duracao_max_servico int not null,
preco_min_servico decimal(10,2) not null,
preco_max_servico decimal(10,2) not null,
foreign key (codigo_empresa) references empresa(codigo_empresa)
);

insert into servicos
(nome_servico, descricao_servico, duracao_min_servico, duracao_max_servico, preco_min_servico, preco_max_servico)
values 
('aromaterapia','massagem com oleos essenciais para relaxamento e equilibrio',30,60,180.00,260.00),
('pedras quentes', 'massagem com pedras vulcanicas aquecidas para relaxamento muscular', 60,60,255.00,255.00),
('shiatsu', 'tecnica oriental com pressao dos dedos em pontos especificos', 45,45,260.00,260.00),
('com ventosa', 'massagem que utiliza ventosas para ativar a circulacao', 30,30,125.00,125.00),
('sueca', 'massagem classica com manobras relaxantes e terapeuticas', 45,45,210.00,210.00),
('desportiva', 'massagem voltada para alivio de tensoes musculares em atletas', 50,80,127.50,280.00),
('esfoliacao completa', 'remocao de celulas mortas com cremes esfoliantes', 60,60,120.00,250.00),
('envoltorio de argilas, chocolates ou algas', 'tratamento nutritivo com aplicacao corporal de elementos naturais', 90,90,690.00,690.00),
('limpeza de pele completa', 'limpeza profunda com extracao de impurezas e hidratacao facial', 60,60,133.00,133.00),
('hidratacao profunda e mascaras faciais nutritivas', 'tratamento facial com aplicacao de mascaras e hidratacao intensa', 60,60,140.00,140.00),
('massagem facial', 'massagem relaxante e estimulante para a face', 30,30,130.00,130.00),
('sauna seca ou a vapor', 'acesso a sauna seca ou umida para relaxamento', 30,30,50.00,80.00),
('banho com sais de banho', 'imersao relaxante com sais especiais', 30,30,100.00,150.00);

create table if not exists pacotes (
codigo_pacote int auto_increment primary key,
codigo_empresa int not null,
nome_pacote varchar(100) not null,
descricao_pacote text not null,
preco_pacote decimal(10,2) not null,
duracao_pacote int not null,
valor_por_pessoa_extra_pacote decimal(10,2) not null default 0.00,
foreign key (codigo_empresa) references empresa(codigo_empresa)
);


insert into pacotes 
(nome_pacote, descricao_pacote, preco_pacote, duracao_pacote, valor_por_pessoa_extra)
values 
('spa day dois ou mais', 'escalda pes aromatico, hidratacao profunda, massagem facial, massagem relaxante corporal, shiatsu', 813.00, 180, 150.00),
('spa day individual', 'ritual de recepcao, esfoliacao corporal com lama quente e oleos essenciais, escalda pes aromatico, massagem corporal com pedras quentes, hidratacao profunda, massagem facial relaxante', 713.00, 240, 0.00);

create table if not exists itens_pacote (
codigo_itens_pacote int auto_increment primary key,
codigo_pacote int not null,
codigo_servico int not null,
ordem_execucao_itens_pacote int not null,
quantidade_itens_pacote int not null,
foreign key (codigo_pacote) references pacotes(codigo_pacote),
foreign key (codigo_servico) references servicos(codigo_servico)
);

insert into itens_pacote 
(codigo_pacote, nome_servico_itens_pacote, ordem_execucao_itens_pacote, quantidade_itens_pacote)
values 
(1, 'banho com sais de banho', 1, 1),
(1, 'hidratacao profunda e mascaras faciais nutritivas', 2, 1),
(1, 'massagem facial', 3, 1),
(1, 'sueca', 4, 1),
(1, 'shiatsu', 5, 1),
(2, 'esfoliacao completa', 1, 1),
(2, 'banho com sais de banho', 2, 1),
(2, 'pedras quentes', 3, 1),
(2, 'hidratacao profunda e mascaras faciais nutritivas', 4, 1),
(2, 'massagem facial', 5, 1);

create table if not exists produtos_estoque (
codigo_produto int auto_increment not null primary key,
codigo_empresa int not null,
nome_produto_estoque varchar(100) not null,
categoria_produto_estoque varchar(50) not null,
quantidade_produto_estoque int not null,
preco_unitario_produto_estoque decimal(10,2) not null,
preco_total_produto_estoque decimal(10,2) not null,
fornecedor_produto_estoque varchar(100) not null,
foreign key (codigo_empresa) references empresa(codigo_empresa)
);

insert into produtos_estoque 
(nome_produto, categoria_produto, quantidade_produto, preco_unitario_produto, preco_total_produto, fornecedor)
values 
('maca dobravel', 'equipamento', 1, 1310.00, 1310.00, 'andrey'),
('kit bolsa termica com pedras quentes', 'equipamento', 1, 163.33, 163.33, 'mercado livre'),
('kit 12 ventosas', 'acessorio', 1, 47.99, 47.99, 'mercado livre'),
('alcool 70 500ml', 'limpeza', 2, 9.28, 18.56, 'magazine luiza'),
('agua sanitaria 1l', 'limpeza', 2, 5.20, 10.40, 'amazon'),
('mascara descartavel cem unidades', 'protecao', 1, 38.51, 38.51, 'magazine luiza'),
('aquecedor de pedras quentes', 'equipamento', 1, 385.90, 385.90, 'amazon'),
('vela aromatica maracuja com baunilha', 'aromatizante', 5, 21.90, 109.50, 'yalume'),
('sais de banho esfoliante romance', 'cosmetico', 2, 89.00, 178.00, 'feito brasil'),
('cerave locao hidratante corporal', 'cosmetico', 2, 88.60, 177.20, 'amazon'),
('locao hidratante nativa spa ameixa negra', 'cosmetico', 1, 63.00, 63.00, 'amazon'),
('toalha banho hotel 100% algodao', 'toalhas', 2, 157.90, 315.80, 'amazon'),
('roupao cortex microfibra', 'vestuario', 8, 56.31, 450.48, 'amazon'),
('oleo de massagem vegetal', 'massagem', 2, 59.90, 119.80, 'kalya tantra'),
('argilas corporais e faciais', 'cosmetico', 3, 33.00, 99.00, 'mercado livre'),
('kit mascaras nutritivas', 'cosmetico', 1, 188.73, 188.73, 'mercado livre'),
('creme facial creamy calming', 'cosmetico', 2, 46.90, 93.80, 'gendo erp');

create table if not exists financeiro (
codigo_financeiro int auto_increment not null primary key,
codigo_empresa int not null,
tipo_financeiro enum('receita', 'despesa') not null,
descricao_financeiro text not null,
valor_financeiro decimal(10,2) not null,
data_financeiro date not null,
foreign key (codigo_empresa) references empresa(codigo_empresa)
);

insert into financeiro(tipo_financeiro, descricao_financeiro, valor_financeiro, data_financeiro)
values
('despesa','aluguel do espaco comercial',1200.00,'2025-05-01'),
('despesa','conta de energia eletrica',1400.00,'2025-05-01'),
('despesa','conta de agua',220.00,'2025-05-01');

create table if not exists funcionarios (
codigo_funcionario int auto_increment not null primary key,
codigo_empresa int not null,
nome_funcionario varchar(100) not null,
cargo_funcionario varchar(50) not null,
telefone_funcionario varchar(15) not null,
email_funcionario varchar(100) not null,
foreign key (codigo_empresa) references empresa(codigo_empresa)
);

insert into funcionarios(nome_funcionario,cargo_funcionario,telefone_funcionario,email_funcionario)
values 
('eduarda da rocha','massagista','1193247875','eduarda.rocha@gmail.com'),
('heloisa marques pereira','gerente','11957888841','marques.heloisa@gmail.com'),
('mariana yukimi ishigaki','socio e recepcionista','11923479675','mariana.yukimi@gmail.com');

create table if not exists clientes (
codigo_cliente int auto_increment not null primary key,
codigo_empresa int not null,
nome_cliente varchar(200) not null,
telefone_cliente varchar(20) not null,
email_cliente varchar(200) not null,
nascimento_cliente date not null,
sexo_cliente varchar(20) not null,
foreign key (codigo_empresa) references empresa(codigo_empresa)
);

insert into clientes(nome_cliente, telefone_cliente, email_cliente, nascimento_cliente, sexo_cliente)
values 
('mirela silva aparecida','11962268712','nascil.rela@gmail.com','1998-08-03','feminino');

create table if not exists agendamentos (
codigo_agendamento int auto_increment not null primary key,
codigo_empresa int not null,
codigo_cliente int not null,
codigo_funcionario int not null,
codigo_servico int not null,
data date not null,
duracao_agendamento time,
status enum('agendado','concluído','cancelado') not null,
foreign key (codigo_empresa) references empresa(codigo_empresa),
foreign key (codigo_cliente) references clientes(codigo_cliente),
foreign key (codigo_funcionario) references funcionarios(codigo_funcionario),
foreign key (codigo_servico) references servicos(codigo_servico)
);
    
drop table if exists itens_pacote;
drop table if exists pacotes;
drop table if exists servicos;
drop table if exists empresa;
drop table if exists produtos_estoque;
drop table if exists financeiro;
drop table if exists funcionarios;
drop table if exists clientes;
drop table if exists agendamentos;

select * from empresa;
select * from servicos;
select * from pacotes;
select * from itens_pacote;
select * from produtos_estoque;
select * from financeiro;
select * from funcionarios;
select * from clientes;
select * from agendamentos;

desc empresa;
desc servicos;
desc pacotes;
desc itens_pacote;
desc produtos_estoque;
desc financeiro;
desc funcionarios;
desc clientes;
desc agendamentos;

select pacotes.nome_pacote, itens_pacote.nome_servico_itens_pacote
from pacotes
inner join itens_pacote
on pacotes.codigo_pacote = itens_pacote.codigo_pacote
inner join servicos
on itens_pacote.nome_servico_itens_pacote = servicos.nome_servico;

select pacotes.nome_pacote, itens_pacote.nome_servico_itens_pacote
from pacotes
left outer join itens_pacote
on pacotes.codigo_pacote = itens_pacote.codigo_pacote
left outer join servicos
on itens_pacote.nome_servico_itens_pacote = servicos.nome_servico;

select pacotes.nome_pacote, servicos.nome_servico
from pacotes
right outer join itens_pacote
on pacotes.codigo_pacote = itens_pacote.codigo_pacote
right outer join servicos
on itens_pacote.nome_servico_itens_pacote = servicos.nome_servico;

select pacotes.nome_pacote, servicos.nome_servico
from pacotes
left outer join itens_pacote
on pacotes.codigo_pacote = itens_pacote.codigo_pacote
left outer join servicos
on itens_pacote.nome_servico_itens_pacote = servicos.nome_servico
union
select pacotes.nome_pacote, servicos.nome_servico
from pacotes
right outer join itens_pacote
on pacotes.codigo_pacote = itens_pacote.codigo_pacote
right outer join servicos
on itens_pacote.nome_servico_itens_pacote = servicos.nome_servico;

select agendamentos.codigo_agendamento, clientes.nome_cliente, funcionarios.nome_funcionario, servicos.nome_servico
from agendamentos
inner join clientes
on agendamentos.codigo_cliente = clientes.codigo_cliente
inner join funcionarios
on agendamentos.codigo_funcionario = funcionarios.codigo_funcionario
inner join servicos
on agendamentos.codigo_servico = servicos.codigo_servico;

select clientes.nome_cliente, agendamentos.codigo_agendamento, funcionarios.nome_funcionario, servicos.nome_servico
from clientes
left outer join agendamentos
on clientes.codigo_cliente = agendamentos.codigo_cliente
left outer join funcionarios
on agendamentos.codigo_funcionario = funcionarios.codigo_funcionario
left outer join servicos
on agendamentos.codigo_servico = servicos.codigo_servico;

select clientes.nome_cliente, agendamentos.codigo_agendamento, funcionarios.nome_funcionario, servicos.nome_servico
from clientes
right outer join agendamentos
on clientes.codigo_cliente = agendamentos.codigo_cliente
right outer join funcionarios
on agendamentos.codigo_funcionario = funcionarios.codigo_funcionario
right outer join servicos
on agendamentos.codigo_servico = servicos.codigo_servico;
