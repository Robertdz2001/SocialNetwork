export const baseUrl = "https://localhost:7229/api";

export const authorization = (token) => {
    return {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    };
  };