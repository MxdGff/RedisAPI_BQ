@echo on 
color 2f
mode con:cols=80 lines=25
@REM 
@echo 正在清理SVN文件........

@rem 循环删除当前目录和子目录下的文件
@rem for /r .%%a in (.) do @if exist "%%a\.svn" @echo "%%a\.svn"
@rem for /r . %%a in (.) do @if exist "%%a\.svn" rd /s /q "%%a\.svn"
@for /r . %%a in (.) do @if exist "%%a\.vs" rd /s /q "%%a\.vs"
@for /r . %%a in (.) do @if exist "%%a\.user" rd /s /q "%%a\.user"
@for /r . %%a in (.) do @if exist "%%a\bin" rd /s /q "%%a\bin"
@for /r . %%a in (.) do @if exist "%%a\obj" rd /s /q "%%a\obj"
@for /r . %%a in (.) do @if exist "%%a\packages" rd /s /q "%%a\packages"
@for /r . %%a in (.) do @if exist "%%a\dist" rd /s /q "%%a\dist"
@for /r . %%a in (.) do @if exist "%%a\node_modules" rd /s /q "%%a\node_modules"

@echo 清理完毕
@pause