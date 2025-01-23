import { logger } from "@/utils/Logger.js"
import { api } from "./AxiosService.js"
import { Favorite } from "@/models/Favorite.js"
import { AppState } from "@/AppState.js"


class FavoritesService {
    async getAccountFavorites() {
        const response = await api.get('account/favorites')
        logger.log('my favorite profiles', response.data)
    }
    async createFavorite(favoriteData) {
        const response = await api.post('api/favorites', favoriteData)
        logger.log('favoriting recipe', response.data)
        const favorites = new Favorite(response.data)
        AppState.favorites.push(favorites)
    }

}


export const favoritesService = new FavoritesService()