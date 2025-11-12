import type { CreateItemToDo, TodoItem } from "../../types/Todo.types";
import { apiClient } from "../apiClient";

export const getTodoItems = async (): Promise<TodoItem[]> => {
  const response = await apiClient.get("/api/TodoItems");
  return response.data;
};

export const addTodoItem = async (
  newTodo: CreateItemToDo,
): Promise<TodoItem> => {
  const response = await apiClient.post("/api/TodoItems", newTodo);
  return response.data;
};

export const updateTodoItem = async (
  id: number,
  updatedTodo: TodoItem,
): Promise<void> => {
  await apiClient.put(`/api/TodoItems/${id}`, updatedTodo);
};

export const deleteTodoItem = async (id: number): Promise<void> => {
  await apiClient.delete(`/api/TodoItems/${id}`);
};
