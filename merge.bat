git config --global user.email "greatmsio@yahoo.es"
git config --global user.name "GeMe"

REM git commit -a -m "increase version"

REM @echo off
REM ECHO SOURCE BRANCH IS %BUILD_SOURCEBRANCH%
REM IF %BUILD_SOURCEBRANCH% == refs/heads/master (
REM    ECHO Building master branch so no merge is needed.
REM    EXIT
REM )

SET sourceBranch=origin/%BUILD_SOURCEBRANCH:refs/heads/=%
ECHO GIT CHECKOUT MASTER
git checkout master
REM ECHO GIT STATUS
REM git status
REM ECHO GIT ADD FILES
REM git add .
REM git commit -m "increase version [ci skip]"
REM ECHO GIT MERGE
REM git merge %sourceBranch% -m "increase version [ci skip]"
REM ECHO GIT STATUS
REM git status
REM ECHO GIT PUSH
REM git push origin
REM ECHO GIT PUSH
REM git push origin
REM ECHO GIT STATUS
REM git status