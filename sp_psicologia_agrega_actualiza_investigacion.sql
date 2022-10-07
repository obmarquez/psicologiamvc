USE [Integral]
GO

/****** Object:  StoredProcedure [dbo].[sp_psicologia_agrega_actualiza_investigacion]    Script Date: 01/02/2021 09:35:37 p. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		Oscar Bolaños Marquez
-- Create date: 23/03/2020
-- Description:	Agrega o actualiza la investigación psicologica
-- =============================================
CREATE PROCEDURE [dbo].[sp_psicologia_agrega_actualiza_investigacion]
	-- Add the parameters for the stored procedure here
	@idh int,						-- idh de Historico para obtener la curp y el idhistorico
	--@idhistorico int,				-- valor consultado por función
	--@curp_evaluado varchar(10),		-- valor consultado por función
	--@cBender varchar(40),
	--@cDescdano varchar(200),
	@cDiagnostico varchar(3000),
	@bMachover bit,
	@bFigura bit,
	@bMerril bit,
	@bMmpi bit,
	@bAutobiografia bit,
	@cCi varchar(15),
	@cLaboralpsi varchar(1500),
	@cRecomendaciones varchar(1500),
	@cResultado varchar(30),
	@bConcluido bit,
	@clave_psicologo varchar(10),
	@cLugarnac varchar(40),
	@cGrado varchar(10),
	@cEscolaridad varchar(30),
	@bCleaver bit,
	@bTest bit,
	@bRaven bit,
	@bBeta bit,
	@fEvalpsi smalldatetime,
	@bLuscher bit,
	@idSupPsi varchar(10),
	@bHtp bit,
	@bSacks bit,
	@b16fp bit,
	@bBetaiiifr bit,
	@cCompetencia varchar(20),
	@bJuicio bit,
	@bAutoestima bit,
	@bManejo bit,
	@bAlteraciones bit, 
	@bApego bit,
	@bTolerancia bit,
	@bRelaciones bit,
	@fortaleza_riesgo varchar(6000),
	@notas varchar(1000),
	@Result varchar(10) output,			-- Mensaje del resultado de la accion realizada
	@accion int,
	@bMoca bit

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	declare @curp varchar(18)
	declare @idhistorico int

	set @curp = dbo.sp_get_curp_idhistorico(@idh)
	set @idhistorico = dbo.sp_get_idevaluacion_idhistorico(@idh)

	BEGIN TRANSACTION

		if(@accion = 1)
			begin
				insert into tPsicologia01(idhistorico, 	curp_evaluado, cDiagnostico, bMachover, bFigura, bMerril, bMmpi, bAutobiografia, cCi, cLaboralpsi, 
					cRecomendaciones, cResultado, bConcluido, fecha_evaluacion, clave_psicologo, cLugarnac, cGrado, cEscolaridad, bCleaver, bTest, bRaven, 
					bBeta, fEvalpsi, bLuscher, idSupPsi, bHtp, bSacks, b16fp, bBetaiiifr, cCompetencia, bJuicio, bAutoestima, bManejo, bAlteraciones, bApego, 
					bTolerancia, bRelaciones, fortaleza_riesgo, notas, bMoca)
				values(@idhistorico, @curp, @cDiagnostico, @bMachover, @bFigura, @bMerril, @bMmpi, @bAutobiografia, @cCi, @cLaboralpsi,
					@cRecomendaciones, @cResultado, @bConcluido, getdate(), @clave_psicologo, @cLugarnac, @cGrado, @cEscolaridad, @bCleaver, @bTest, @bRaven,
					@bBeta, @fEvalpsi, @bLuscher, @idSupPsi, @bHtp, @bSacks, @b16fp, @bBetaiiifr, @cCompetencia, @bJuicio, @bAutoestima, @bManejo, @bAlteraciones, 
					@bApego, @bTolerancia, @bRelaciones, @fortaleza_riesgo, @notas, @bMoca)

				update thistorico set estatuspsi = 2 where idhistorico = @idh

				IF @@Error>0
					Begin
						RaisError('Error[1]', 16, 1)
						Rollback Transaction
						Return
					END
				Set @Result = 'Ok'
			end
		else
			begin
				update tPsicologia01 set cDiagnostico=@cDiagnostico, bMachover=@bMachover, bFigura=@bFigura, bMerril=@bMerril, bMmpi=@bMmpi, 
										 bAutobiografia=@bAutobiografia, cCi=@cCi, cLaboralpsi=@cLaboralpsi, cRecomendaciones=@cRecomendaciones, 
										 cResultado=@cResultado, bConcluido=@bConcluido, fecha_modifica=getdate(), clave_psicologo_mod=@clave_psicologo, 
										 cLugarnac=@cLugarnac, cGrado=@cGrado, cEscolaridad=@cEscolaridad, bCleaver=@bCleaver, bTest=@bTest, bRaven=@bRaven, 
										 bBeta=@bBeta, fEvalpsi=@fEvalpsi, bLuscher=@bLuscher, idSupPsi=@idSupPsi, bHtp=@bHtp, bSacks=@bSacks, b16fp=@b16fp, 
										 bBetaiiifr=@bBetaiiifr, cCompetencia=@cCompetencia, bJuicio=@bJuicio, bAutoestima=@bAutoestima, bManejo=@bManejo, 
										 bAlteraciones=@bAlteraciones, bApego=@bApego, bTolerancia=@bTolerancia, bRelaciones=@bRelaciones, 
										 fortaleza_riesgo=@fortaleza_riesgo, notas=@notas, bMoca=@bMoca 
				where curp_evaluado=@curp and idhistorico=@idhistorico

				IF @@Error>0
					Begin
						RaisError('Error[1]', 16, 1)
						Rollback Transaction
						Return
					END
				Set @Result = 'Ok'
			end

	COMMIT TRANSACTION
    	
END





GO

