/**
 * Handler that will be called during the execution of a PostUserRegistration flow.
 *
 * @param {Event} event - Details about the context and user that has registered.
 * @param {PostUserRegistrationAPI} api - Methods and utilities to help change the behavior after a signup.
 */
const axios = require("axios");

exports.onExecutePostUserRegistration = async (event, api) => {
    await axios.post("https://b92e-89-64-87-109.ngrok-free.app/api/users", {
        'auth0UserIdentifier': event.user.user_id,
        'username': event.user.email
    }, {
        headers: {
            'Authorization': `ApiKey ${event.secrets.apiKey}`
        }
    });
};