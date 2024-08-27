CREATE TABLE UsuarioPerfil(
PerfilID int,
salario varchar(10),
foreign key (PerfilID) references usuario(UsuarioID)
);