document.addEventListener("DOMContentLoaded", () => {
  const messageContainer = document.getElementById("messageContainer");
  const messageInput = document.getElementById("messageInput");
  const sendMessageButton = document.getElementById("sendMessageButton");

  const currentUser = {
    id: 1,
    avatar: "images/avatar/4.jpg",
  };

  const otherUser = {
    id: 2,
    avatar: "images/avatar/5.jpg",
  };

  //const currentUser = {
  //  id: 2,
  //  avatar: "images/avatar/5.jpg", 
  //};

  //const otherUser = {
  //  id: 1,
  //  avatar: "images/avatar/4.jpg", 
  //};

  // Load messages from localStorage
  const loadMessages = () => {
    const messages = JSON.parse(localStorage.getItem("messages")) || [];
    messageContainer.innerHTML = ""; 

    messages.forEach((message) => {
      const isSender = message.sender_id === currentUser.id;

      const messageHTML = `
                <div class="d-flex justify-content-${isSender ? "end" : "start"} mb-4">
                    ${isSender ? "" : `<div class="img_cont_msg">
                        <img src="${message.avatar_url}" class="rounded-circle user_img_msg" alt="">
                    </div>`}
                    <div class="msg_cotainer${isSender ? "_send" : ""}">
                        ${message.content}
                        <span class="msg_time${isSender ? "_send" : ""}">${message.datetime}</span>
                    </div>
                    ${isSender ? `<div class="img_cont_msg">
                        <img src="${message.avatar_url}" class="rounded-circle user_img_msg" alt="">
                    </div>` : ""}
                </div>
            `;

      messageContainer.innerHTML += messageHTML;
    });

    // Scroll to the bottom
    messageContainer.scrollTop = messageContainer.scrollHeight;
  };

  // Save a new message to localStorage
  const saveMessage = (content) => {
    const messages = JSON.parse(localStorage.getItem("messages")) || [];
    const newMessage = {
      message_id: messages.length + 1,
      sender_id: currentUser.id,
      receiver_id: otherUser.id,
      avatar_url: currentUser.avatar,
      content,
      datetime: new Date().toLocaleString(),
    };

    messages.push(newMessage);
    localStorage.setItem("messages", JSON.stringify(messages));
    loadMessages();
  };

  // Event listener for sending messages
  sendMessageButton.addEventListener("click", () => {
    const messageContent = messageInput.value.trim();
    if (messageContent) {
      saveMessage(messageContent);
      messageInput.value = ""; // Clear input field
    }
  });

  // Initial load
  loadMessages();
});
