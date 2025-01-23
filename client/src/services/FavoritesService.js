import { logger } from "@/utils/Logger.js"
import { api } from "./AxiosService.js"
import { Favorite } from "@/models/Favorite.js"
import { AppState } from "@/AppState.js"


class FavoritesService {
    async deleteFavorite(favoriteId) {
        const response = await api.delete(`api/favorites/${favoriteId}`)
        logger.log(`deleting favorite ID of ${favoriteId}`, response.data)
        const favToDelete = AppState.favorites.findIndex(favorite => favorite.favoriteId == favoriteId)
        AppState.favorites.splice(favToDelete, 1)
        
    }
    async getAccountFavorites() {
        const response = await api.get('account/favorites')
        logger.log('my favorite profiles', response.data)
        const favorites = response.data.map(pojo => new Favorite(pojo))
        AppState.favorites = favorites
    }
    async createFavorite(favoriteData) {
        const response = await api.post('api/favorites', favoriteData)
        logger.log('favoriting recipe', response.data)
        const favorites = new Favorite(response.data)
        AppState.favorites.push(favorites)
    }

}


export const favoritesService = new FavoritesService()