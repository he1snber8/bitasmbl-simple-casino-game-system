import { useNavigate } from "react-router-dom";
import { IoArrowBack } from "react-icons/io5";

const NotFoundPage = () => {
  const navigate = useNavigate();

  return (
    <div className="h-screen flex flex-col items-center justify-center bg-[#0f0f0f] text-white text-center px-6">
      <h1 className="text-6xl font-bold text-purple-500 mb-4">404</h1>
      
      <p className="text-gray-400 max-w-md mb-6">
        Game
      </p>
      <button
        onClick={() => navigate(-1)}
        className="flex items-center gap-2 bg-purple-600 hover:bg-purple-700 text-white px-5 py-2 rounded-full transition"
      >
        <IoArrowBack />
        Go Back
      </button>
    </div>
  );
};

export default NotFoundPage;
