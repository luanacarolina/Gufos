USE Gufos;

INSERT INTO Tipo_Usuario   (Titulo)
VALUES
							('Administrador'),
							('Aluno')
							
INSERT INTO Usuario(Nome ,Email , Senha , Tipo_usuario_id)
VALUES
					('Administrador' , 'adm@adm.com' , '123',1),
					('Ariel','ariel@email.com','123',2)


INSERT INTO Localizacao (CNPJ , Razao_social , Endereco)
VALUES
						('1234567812345','Escola SENAI de Informatica','Al.Bar�o de Limeira,539')

							
INSERT INTO Categoria   (Titulo)
VALUES						('Desenvolvimento'),
						('HTML + CSS'),
						('Marketing')

INSERT INTO Evento    (Titulo, Categoria_id , Acesso_livre, Data_evento , Localizacao_id)
VALUES ('C#' , 2,0 ,'2019-08-07T18:00:00' ,1),
		('Estrutura Sem�ntica' , 2, 1,GETDATE(),1 )



INSERT INTO Presenca (Evento_id ,Usuario_id ,Presenca_status)
VALUES				(1,2 ,'Aguardando'),
					(1,1 ,'Confirmado')
		SELECT * FROM Evento