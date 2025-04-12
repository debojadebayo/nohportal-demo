docker stop `docker ps -qa`
docker rm `docker ps -qa`
docker rmi -f `docker images -qa `
docker volume rm $(docker volume ls -qf)
docker network rm `docker network ls -q`

# docker ps -a
# docker images -a 
# docker volume ls
# docker network ls
