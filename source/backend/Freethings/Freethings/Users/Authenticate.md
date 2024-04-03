## How to authenticate a user

To authenticate a user, you need to:

1. Open URL below in a browser:

```
https://dev-vf8eeqf11x3ud8nu.us.auth0.com/authorize?client_id=j8CdPORxwFKz9oiiCy2rNsZ1URxtauBh&response_type=code&prompt=login&scope=openid%20profile&redirect_uri=http://localhost:3000/callback&state=1234zyx
```

2. Enter credentials.
3. You will be redirected to `http://localhost:3000/callback` with a code in the query string.
4. Exchange the code for an access token by sending a POST request to the token endpoint:

```curl
curl --request POST \
  --url 'https://dev-vf8eeqf11x3ud8nu.us.auth0.com/oauth/token' \
  --header 'content-type: application/x-www-form-urlencoded' \
  --data grant_type=client_credentials \
  --data client_id=j8CdPORxwFKz9oiiCy2rNsZ1URxtauBh \
  --data client_secret=YOUR_CLIENT_SECRET \
  --data audience=YOUR_API_IDENTIFIER
```