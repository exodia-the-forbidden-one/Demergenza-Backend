FROM nginx

# create ssl certificate and key
RUN mkdir -p /etc/nginx/ssl \
    && openssl req -x509 -nodes -days 365 -newkey rsa:2048 \
    -keyout /etc/nginx/ssl/ssl.key -out /etc/nginx/ssl/ssl.crt \
    -subj "/C=TR/L=City/O=Demergenza/CN=demergenza.com"

# COPY ssl.crt /etc/nginx/ssl/ssl.crt
# COPY ssl.key /etc/nginx/ssl/ssl.key
RUN openssl pkcs12 -export -in /etc/nginx/ssl/ssl.crt -inkey /etc/nginx/ssl/ssl.key -out /etc/nginx/ssl/ssl.pfx -passout pass:mysecretkey

COPY nginx/conf/nginx.conf /etc/nginx/nginx.conf

CMD ["nginx", "-g", "daemon off;"]
