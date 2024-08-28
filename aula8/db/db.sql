-- UNION: selecionar usuários com perfil 1 e 2, sem duplicatas
SELECT u.nome, u.email
FROM usuario u
JOIN usuarioperfil up ON u.UsuarioID = up.PerfilID
WHERE up.PerfilID = 1
UNION
SELECT u.nome, u.email
FROM usuario u
JOIN usuarioperfil up ON u.UsuarioID = up.PerfilID
WHERE up.PerfilID = 2;