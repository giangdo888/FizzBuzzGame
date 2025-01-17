const API_ADDRESS = process.env.NODE_ENV === "production"
    ? "http://localhost:8080"
    : "https://localhost:7044";

export async function fetchData(
    endpoint: string,
    options?: {
        method: string,
        body: object
    }
) {
    try {
        const response = await fetch(`${API_ADDRESS}/${endpoint}`, {
            method: options?.method || "GET",
            body: options?.method !== "GET" && options?.body ? JSON.stringify(options.body) : undefined,
            headers: {
                "Content-Type": "application/json"
            }
        });

        if (!response.ok) {
            throw new Error(`Error with http status: ${response.status}`);
        }

        return await response.json();
    } catch (error) {
        console.error("Fetch error : " + error);
        return null;
    }
}