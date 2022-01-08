FROM rabbitmq:3.8.17-management

COPY ./rabbitmq_delayed_message_exchange-3.8.17.8f537ac.ez /opt/rabbitmq/plugins/

RUN rabbitmq-plugins enable rabbitmq_delayed_message_exchange