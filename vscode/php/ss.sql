drop database if exists ss;
create database if not exists ss;
use ss;

create table if not exists empresa (
codigo_empresa int auto_increment not null,
razao_social varchar(200) not null,
nome_fantasia varchar(200) not null,
cnpj_empresa char(14) not null unique,
localizacao_empresa varchar(255) not null,
telefone_empresa char(11) not null,
email_empresa varchar(150) not null,
site_empresa varchar(150),
primary key (codigo_empresa)
);

insert into empresa (razao_social, nome_fantasia, cnpj_empresa, localizacao_empresa, telefone_empresa, email_empresa, site_empresa)
values 
('serenity spa e massagens ltda', 'serenity spa', '93911881000183', 'rua pedro bibiano, jardim barcelona, presidente prudente - sp', '11986288806', 'contato@serenityspa.com', 'serenityspa.com.br');

create table if not exists servicos (
codigo_servico int auto_increment not null,
codigo_empresa int not null,
nome_servico varchar(100) not null,
descricao_servico varchar(255) not null,
duracao_min_servico int not null,
duracao_max_servico int not null,
preco_min_servico decimal(10,2) not null,
preco_max_servico decimal(10,2) not null,
primary key (codigo_servico),
constraint fk_empresa_serv foreign key (codigo_empresa)
references empresa(codigo_empresa)
on delete cascade
);

insert into servicos (codigo_empresa, nome_servico, descricao_servico, duracao_min_servico, duracao_max_servico, preco_min_servico, preco_max_servico)
values
(1, 'aromaterapia','massagem com óleos essenciais para relaxamento',30,60,180.00,260.00),
(1, 'shiatsu','técnica oriental com pressão dos dedos',45,45,260.00,260.00),
(1, 'pedras quentes','massagem com pedras vulcânicas aquecidas',60,60,255.00,255.00);

create table if not exists pacotes (
codigo_pacote int auto_increment not null,
codigo_empresa int not null,
nome_pacote varchar(150) not null,
descricao_pacote varchar(255) not null,
preco_pacote decimal(10,2) not null,
duracao_pacote int not null,
valor_por_pessoa_extra decimal(10,2),
primary key (codigo_pacote),
constraint fk_empresa_pac foreign key (codigo_empresa)
references empresa(codigo_empresa)
on delete cascade
);

insert into pacotes (codigo_empresa, nome_pacote, descricao_pacote, preco_pacote, duracao_pacote, valor_por_pessoa_extra)
values
(1, 'spa day individual','massagem corporal e facial completa',713.00,240,0.00),
(1, 'spa day duplo','massagem relaxante para duas pessoas',813.00,180,150.00);

create table if not exists itens_pacote (
codigo_item int auto_increment not null,
codigo_pacote int not null,
codigo_servico int not null,
ordem_execucao int not null,
quantidade int not null,
primary key (codigo_item),
constraint fk_pacote foreign key (codigo_pacote)
references pacotes(codigo_pacote)
on delete cascade,
constraint fk_servico foreign key (codigo_servico)
references servicos(codigo_servico)
on delete cascade
);

insert into itens_pacote (codigo_pacote, codigo_servico, ordem_execucao, quantidade)
values
(1,1,1,1),
(1,2,2,1),
(2,3,1,1);

create table if not exists produtos_estoque (
codigo_produto int auto_increment not null,
codigo_empresa int not null,
nome_produto varchar(150) not null,
categoria varchar(100) not null,
quantidade int not null,
preco_unitario decimal(10,2) not null,
preco_total decimal(10,2) not null,
fornecedor varchar(150) not null,
primary key (codigo_produto),
constraint fk_empresa_prod foreign key (codigo_empresa)
references empresa(codigo_empresa)
on delete cascade
);

insert into produtos_estoque (codigo_empresa, nome_produto, categoria, quantidade, preco_unitario, preco_total, fornecedor)
values
(1, 'maca dobrável','equipamento',1,1310.00,1310.00,'andrey'),
(1, 'kit pedras quentes','equipamento',1,163.00,163.00,'mercado livre');

create table if not exists financeiro (
codigo_financeiro int auto_increment not null,
codigo_empresa int not null,
tipo varchar(20) not null,
descricao varchar(255) not null,
valor decimal(10,2) not null,
data_financeiro date not null,
primary key (codigo_financeiro),
constraint fk_empresa_fin foreign key (codigo_empresa)
references empresa(codigo_empresa)
on delete cascade
);

insert into financeiro (codigo_empresa, tipo, descricao, valor, data_financeiro)
values
(1,'despesa','aluguel do espaço',1200.00,'2025-05-01'),
(1,'despesa','energia elétrica',1400.00,'2025-05-01');

create table if not exists funcionarios (
codigo_funcionario int auto_increment not null,
codigo_empresa int not null,
nome_funcionario varchar(150) not null,
cargo_funcionario varchar(100) not null,
telefone_funcionario char(11) not null,
email_funcionario varchar(150) not null,
primary key (codigo_funcionario),
constraint fk_empresa_func foreign key (codigo_empresa)
references empresa(codigo_empresa)
on delete cascade
);

insert into funcionarios (codigo_empresa, nome_funcionario, cargo_funcionario, telefone_funcionario, email_funcionario)
values
(1,'eduarda da rocha','massagista','1193247875','eduarda.rocha@gmail.com'),
(1,'heloisa marques','gerente','11957888841','heloisa.marques@gmail.com');

create table if not exists clientes (
codigo_cliente int auto_increment not null,
codigo_empresa int not null,
nome_cliente varchar(200) not null,
telefone_cliente char(11) not null,
email_cliente varchar(200),
nascimento_cliente date not null,
sexo_cliente varchar(20) not null,
primary key (codigo_cliente),
constraint fk_empresa_cli foreign key (codigo_empresa)
references empresa(codigo_empresa)
on delete cascade
);

insert into clientes (codigo_empresa, nome_cliente, telefone_cliente, email_cliente, nascimento_cliente, sexo_cliente)
values
(1,'mirela silva','11962268712','mirela@gmail.com','1998-08-03','feminino');

create table if not exists agendamentos (
codigo_agendamento int auto_increment not null,
codigo_empresa int not null,
codigo_cliente int not null,
codigo_funcionario int not null,
codigo_servico int not null,
data_agendamento date not null,
duracao time,
status varchar(20) not null,
primary key (codigo_agendamento),
constraint fk_empresa_ag foreign key (codigo_empresa)
references empresa(codigo_empresa)
on delete cascade,
constraint fk_cliente_ag foreign key (codigo_cliente)
references clientes(codigo_cliente)
on delete cascade,
constraint fk_funcionario_ag foreign key (codigo_funcionario)
references funcionarios(codigo_funcionario)
on delete cascade,
constraint fk_servico_ag foreign key (codigo_servico)
references servicos(codigo_servico)
on delete cascade
);

select * from empresa;
select * from servicos;
select * from pacotes;
select * from produtos_estoque;
select * from funcionarios;
select * from clientes;

select nome_servico as nome, 'serviço individual' as tipo from servicos
union all
select nome_pacote as nome, 'pacote de serviços' as tipo from pacotes;

select p.nome_pacote, s.nome_servico
from pacotes p
inner join itens_pacote i on p.codigo_pacote = i.codigo_pacote
inner join servicos s on i.codigo_servico = s.codigo_servico;

select p.nome_pacote, s.nome_servico
from pacotes p
left join itens_pacote i on p.codigo_pacote = i.codigo_pacote
left join servicos s on i.codigo_servico = s.codigo_servico;

select p.nome_pacote, s.nome_servico
from pacotes p
right join itens_pacote i on p.codigo_pacote = i.codigo_pacote
right join servicos s on i.codigo_servico = s.codigo_servico;

select p.nome_pacote, s.nome_servico
from pacotes p
left join itens_pacote i on p.codigo_pacote = i.codigo_pacote
left join servicos s on i.codigo_servico = s.codigo_servico
union
select p.nome_pacote, s.nome_servico
from pacotes p
right join itens_pacote i on p.codigo_pacote = i.codigo_pacote
right join servicos s on i.codigo_servico = s.codigo_servico;

select a.codigo_agendamento, c.nome_cliente, f.nome_funcionario, s.nome_servico
from agendamentos a
inner join clientes c on a.codigo_cliente = c.codigo_cliente
inner join funcionarios f on a.codigo_funcionario = f.codigo_funcionario
inner join servicos s on a.codigo_servico = s.codigo_servico;

select c.nome_cliente, a.codigo_agendamento, f.nome_funcionario, s.nome_servico
from clientes c
left join agendamentos a on c.codigo_cliente = a.codigo_cliente
left join funcionarios f on a.codigo_funcionario = f.codigo_funcionario
left join servicos s on a.codigo_servico = s.codigo_servico;

select c.nome_cliente, a.codigo_agendamento, f.nome_funcionario, s.nome_servico
from clientes c
right join agendamentos a on c.codigo_cliente = a.codigo_cliente
right join funcionarios f on a.codigo_funcionario = f.codigo_funcionario
right join servicos s on a.codigo_servico = s.codigo_servico;
