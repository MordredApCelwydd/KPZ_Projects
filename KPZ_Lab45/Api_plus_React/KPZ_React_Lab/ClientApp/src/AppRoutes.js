import { Counter } from "./components/Counter";
import Employees from "./components/Employees";
import { FetchData } from "./components/FetchData";
import { Home } from "./components/Home";
import Projects from "./components/Projects";

const AppRoutes = [
  {
    index: true,
    element: <Home />,
  },
  {
    path: "/counter",
    element: <Counter />,
  },
  {
    path: "/fetch-data",
    element: <FetchData />,
  },
  {
    path: "projects",
    element: <Projects />,
  },
  {
    path: "employees",
    element: <Employees />,
  },
];

export default AppRoutes;
