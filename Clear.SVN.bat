@echo on 
color 2f
mode con:cols=80 lines=25
@REM 
@echo ��������SVN�ļ�........

@rem ѭ��ɾ����ǰĿ¼����Ŀ¼�µ��ļ�
@rem for /r .%%a in (.) do @if exist "%%a\.svn" @echo "%%a\.svn"
@rem for /r . %%a in (.) do @if exist "%%a\.svn" rd /s /q "%%a\.svn"
@for /r . %%a in (.) do @if exist "%%a\.vs" rd /s /q "%%a\.vs"
@for /r . %%a in (.) do @if exist "%%a\.user" rd /s /q "%%a\.user"
@for /r . %%a in (.) do @if exist "%%a\bin" rd /s /q "%%a\bin"
@for /r . %%a in (.) do @if exist "%%a\obj" rd /s /q "%%a\obj"
@for /r . %%a in (.) do @if exist "%%a\packages" rd /s /q "%%a\packages"
@for /r . %%a in (.) do @if exist "%%a\dist" rd /s /q "%%a\dist"
@for /r . %%a in (.) do @if exist "%%a\node_modules" rd /s /q "%%a\node_modules"

@echo �������
@pause