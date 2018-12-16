
ALTER TABLE tab_usuario add id_perfil int null;

ALTER TABLE tab_usuario add FOREIGN KEY (id_perfil) REFERENCES tab_perfil(id);

INSERT INTO tab_perfil (nome, ativo) VALUES ('Gerente',1);

UPDATE tab_usuario SET id_perfil = 3 where id = 3

ALTER TABLE tab_usuario alter column id_perfil int not null

