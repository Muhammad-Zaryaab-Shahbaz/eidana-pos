# Eidana POS

## Deployment Notes

- SSH into the VPS:

```
ssh root@82.29.160.243
```

[Get the password from the administrators]

- Navigate to the directory.

```
cd eidana-pos
```

- Pull latest changes from Github.

```
git pull
```

- Stop the running containers.

```
docker compose down
```

- Start the containers again with latest commits.

```
docker compose up -d --build
```

Happy coding!
