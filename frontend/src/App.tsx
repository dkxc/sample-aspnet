import React, { useState, useEffect } from "react";
import {
  getTodoItems,
  addTodoItem,
  updateTodoItem,
  deleteTodoItem,
} from "./lib/api/todo";
import type { TodoItem } from "./types/Todo.types";

const App: React.FC = () => {
  const [todoItems, setTodoItems] = useState<TodoItem[]>([]);
  const [newTodoName, setNewTodoName] = useState("");
  const [_editingItem, setEditingItem] = useState<TodoItem | null>(null);
  const [error, setError] = useState<string | null>(null);
  const [isLoading, setIsLoading] = useState<boolean>(true);

  useEffect(() => {
    const fetchTodoItems = async () => {
      try {
        setIsLoading(true);
        setError(null);
        const items = await getTodoItems();
        if (Array.isArray(items)) {
          setTodoItems(items);
        } else {
          setTodoItems([]);
          console.warn(
            "API did not return an array for getTodoItems, defaulting to empty array.",
            items
          );
        }
      } catch (error: any) {
        setError(error.message || "Error fetching todo items");
        setTodoItems([]);
        console.error("Error fetching todo items:", error);
      } finally {
        setIsLoading(false);
      }
    };

    fetchTodoItems();
  }, []);

  const handleAddTodo = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!newTodoName.trim()) return;

    try {
      const newItemData = { name: newTodoName, isComplete: false };
      const addedItem = await addTodoItem(newItemData);
      setTodoItems((prevItems) => [...prevItems, addedItem]);
      setNewTodoName("");
    } catch (error: any) {
      setError(error.message || "Error adding todo item");
      console.error("Error adding todo item:", error);
    }
  };

  const handleToggleComplete = (itemToUpdate: TodoItem) => {
    const updatedItem = {
      ...itemToUpdate,
      isComplete: !itemToUpdate.isComplete,
    };
    handleUpdateTodo(updatedItem);
  };

  const handleUpdateTodo = async (itemToUpdate: TodoItem) => {
    try {
      await updateTodoItem(itemToUpdate.id, itemToUpdate);
      setTodoItems(
        todoItems.map((item) =>
          item.id === itemToUpdate.id ? itemToUpdate : item
        )
      );
      setEditingItem(null);
    } catch (error: any) {
      setError(error.message || "Error updating todo item");
      console.error("Error updating todo item:", error);
    }
  };

  const handleDeleteTodo = async (id: number) => {
    const originalItems = [...todoItems];
    setTodoItems(todoItems.filter((item) => item.id !== id));

    try {
      await deleteTodoItem(id);
    } catch (error: any) {
      setError(error.message || "Error deleting todo item");
      console.error("Error deleting todo item:", error);
      setTodoItems(originalItems);
    }
  };

  if (isLoading) return <div>Loading...</div>;

  return (
    <div>
      <h1>Todo List</h1>
      {error && <div className="error-message">{error}</div>}

      <form onSubmit={handleAddTodo}>
        <input
          type="text"
          value={newTodoName}
          onChange={(e) => setNewTodoName(e.target.value)}
          placeholder="What needs to be done?"
        />
        <button type="submit">Add Todo</button>
      </form>

      <ul>
        {todoItems.map((item) => (
          <li key={item.id} className={item.isComplete ? "completed" : ""}>
            <div style={{ display: "flex", gap: 8 }}>
              <div>
                <input
                  type="checkbox"
                  checked={item.isComplete}
                  onChange={() => handleToggleComplete(item)}
                />
                <span>{item.name}</span>
              </div>
              <button onClick={() => handleDeleteTodo(item.id)}>Delete</button>
            </div>
          </li>
        ))}
      </ul>
    </div>
  );
};

export default App;
